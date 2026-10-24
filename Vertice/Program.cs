using Application.AuditLogs;
using Application.AuditLogs.Interfaces;
using Application.Employees;
using Application.Employees.Interfaces;
using Application.PaymentMethods;
using Application.PaymentMethods.Interfaces;
using Application.Products;
using Application.Products.Interfaces;
using Application.ProviderMovements;
using Application.ProviderMovements.Interfaces;
using Application.Providers;
using Application.Providers.Interfaces;
using Application.Sales;
using Application.Sales.Interfaces;
using Application.Sessions;
using Application.Sessions.Interfaces;
using Application.Users;
using Application.Users.Interfaces;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using WinForms;
using WinForms.AdminForms;
using WinForms.Helpers;
using WinForms.PopUps;

namespace Vertice
{
    internal static class Program
    {
        //Variable mantener API
        private static Process? _apiProcess;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Mientras el sistema está abierto existe este mutex: el instalador (InitializeSetup en
            // Instalador\VerticePOS.iss) lo usa para esperar a que se cierre antes de actualizar.
            using var appMutex = new Mutex(false, "VerticePOS_App");

            //contenedor de dependencias
            var host = CreateHostBuilder().Build();

            // Arranca los hosted services (ej. FiscalQueueWorker, la cola de facturación
            // pendiente). No interfiere con el loop de mensajes de WinForms: son independientes.
            host.Start();

            var config = host.Services.GetRequiredService<IConfiguration>();
            StartBackgroundApi(config);

            //migraciones y db
            ApplyMigrations(host.Services);
            SeedDatabase(host.Services);

            if (!EnsureAdminExists(host.Services))
            {
                StopBackgroundApi();
                host.StopAsync().GetAwaiter().GetResult();
                return;
            }

            //UI
            var form = host.Services.GetRequiredService<FormLogin>();
            System.Windows.Forms.Application.Run(form);

            StopBackgroundApi();
            host.StopAsync().GetAwaiter().GetResult();
        }

        private static void StartBackgroundApi(IConfiguration config)
        {
            try
            {
                string? apiPath = config["ApiPath"];

                if (string.IsNullOrWhiteSpace(apiPath) || !File.Exists(apiPath))
                {
                    MessageBox.Show("El servidor del celular no está configurado o la ruta es incorrecta.\n\nPor favor, vaya a la sección 'Ajustes' en el sistema y seleccione el archivo ejecutable de la API.", "Servidor Offline", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var startInfo = new ProcessStartInfo
                {
                    FileName = apiPath,
                    WorkingDirectory = Path.GetDirectoryName(apiPath),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                _apiProcess = Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo iniciar el servicio del escáner móvil: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void StopBackgroundApi()
        {
            try
            {
                if (_apiProcess != null && !_apiProcess.HasExited)
                {
                    _apiProcess.Kill();
                    _apiProcess.Dispose();
                }
            }
            catch { /* Ignoramos si ya se cerró por su cuenta */ }
        }

        private static void SeedDatabase(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    // Verificamos si existe el producto "Comodín"
                    if (!context.Products.Any(p => p.Code == "VARIOS"))
                    {
                        var productoVarios = new Product
                        {
                            Code = "VARIOS",
                            Name = "Varios / Fiambrería",
                            SalePrice = 0,
                            CostPrice = 0,
                            Stock = 999999,
                            IsActive = true,
                            IsOpenPrice = true,
                            LastStockUpdate = DateTime.Now
                        };

                        context.Products.Add(productoVarios);
                        context.SaveChanges();
                    }

                    // El administrador ya no se siembra con usuario y contraseña fijos: si la base
                    // no tiene usuarios, EnsureAdminExists lo pide crear en el primer arranque.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al sembrar datos iniciales: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Primer arranque (base sin usuarios): pide crear el administrador antes del login.
        /// Devuelve false si se cerró sin crearlo, y entonces el sistema no abre.
        /// </summary>
        private static bool EnsureAdminExists(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            try
            {
                if (userService.AnyUserAsync().GetAwaiter().GetResult()) return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo verificar los usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using var form = scope.ServiceProvider.GetRequiredService<FormFirstAdmin>();
            return form.ShowDialog() == DialogResult.OK;
        }

        private static void ApplyMigrations(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error fatal: No se pudo conectar o aplicar las migraciones a la base de datos.\n" +
                        "La aplicación se cerrará.\n\n" +
                        "Error: " + ex.Message,
                        "Error Crítico de Base de Datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    Environment.Exit(1);
                }
            }
        }

        static IHostBuilder CreateHostBuilder() => 
            Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                // Configuración de esta PC (contribuyente, impresora, API). Va después del
                // genérico para que sus valores ganen. Ver LocalSettings.
                Directory.CreateDirectory(LocalSettings.FolderPath);
                config.AddJsonFile(LocalSettings.FilePath, optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                string connectionString;
                
                //PROD
                //Definimos la ruta en ProgramData (para que los datos sobrevivan si desinstalas el exe)
                
                string appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VerticeApp");

                if (!Directory.Exists(appDataFolder))
                {
                    Directory.CreateDirectory(appDataFolder);
                }

                string dbFilePath = Path.Combine(appDataFolder, "VerticeDB.mdf");

                //El "AttachDbFileName" es lo que hace que use el archivo en ProgramData.
                connectionString = $"Server=.\\SQLEXPRESS;Database=VerticeDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;AttachDbFileName={dbFilePath}";
                

                //DEVELOPMENT
                //connectionString = context.Configuration.GetConnectionString("DefaultConnection")!;

                // Inyectam DbContext
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString));

                //Dependency Injection UI
                services.AddTransient<FormLogin>();
                services.AddTransient<FormAdminDashboard>();
                services.AddTransient<UserControlNewSales>();
                services.AddTransient<UserControlEmployees>();
                services.AddTransient<UserControlHistory>();
                services.AddTransient<UserControlLogs>();
                services.AddTransient<UserControlProducts>();
                services.AddTransient<UserControlProviders>();
                services.AddTransient<UserControlProvidersHistory>();
                services.AddTransient<UserControlPaymentMethods>();
                services.AddTransient<UserControlUsers>();
                services.AddTransient<UserControlSettings>();

                services.AddTransient<FormEmployeeEditor>();
                services.AddTransient<FormPaymentMethodEditor>();
                services.AddTransient<FormProductEditor>();
                services.AddTransient<FormProviderEditor>();
                services.AddTransient<FormUserEditor>();
                services.AddTransient<FormSaleDetail>();
                services.AddTransient<FormMovementDetails>();
                services.AddTransient<FormMovementEditor>();
                services.AddTransient<FormRecoverElements>();
                services.AddTransient<FormPendingInvoices>();
                services.AddTransient<FormPrintLabels>();
                services.AddTransient<FormFirstAdmin>();
                services.AddTransient<UpdateService>();
                services.AddTransient<TicketPrinter>();

                //DI Data
                services.AddScoped<IAuditLogRepository, AuditLogRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
                services.AddScoped<IProductRepository, ProductRepository>();
                services.AddScoped<IProviderMovementRepository, ProviderMovementRepository>();
                services.AddScoped<IProviderRepository, ProviderRepository>();
                services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
                services.AddScoped<ISaleRepository, SaleRepository>();
                services.AddScoped<ISessionRepository, SessionRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddSingleton<IUserSessionService, UserSessionService>();
                services.AddSingleton<ILabelQueueService, LabelQueueService>();
                // Singleton a propósito: OpenScreen destruye el UserControl y su scope en cada
                // navegación, así que las ventas en espera no pueden vivir dentro del control.
                services.AddSingleton<ISuspendedSaleService, SuspendedSaleService>();

                //DI App
                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IProductService, ProductService>();
                services.AddScoped<IPaymentMethodService, PaymentMethodService>();
                services.AddScoped<IProviderService, ProviderService>();
                services.AddScoped<IEmployeeService, EmployeeService>();
                services.AddScoped<ISessionService, SessionService>();
                services.AddScoped<ISaleService, SaleService>();
                services.AddHttpClient();
                services.AddScoped<IFiscalService, FiscalServiceArca>();
                services.AddScoped<IFiscalQueueService, FiscalQueueService>();
                services.AddHostedService<FiscalQueueWorker>();
                services.AddScoped<IRepository<FiscalDocument>, SqlRepository<FiscalDocument>>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IProviderMovementService, ProviderMovementService>();
                services.AddScoped<IAuditLogService, AuditLogService>();
            });
    }
}
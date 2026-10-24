using Application.Products;
using Application.Products.Interfaces;
using Domain.RepositoryInterfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Web.Services;
using Application.AuditLogs;
using Application.AuditLogs.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Base de Datos (Le pasamos la conexión del appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Inyección de Dependencias (El motor de la app)
// Repositorios
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

// Servicios
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

// CRÍTICO: El ProductService necesita un IUserSessionService.
// Como acá no hay pantalla de Login, le pasamos una implementación hardcodeada temporal.
builder.Services.AddScoped<IUserSessionService, ApiDummySessionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ATENCIÓN: Comentamos la redirección HTTPS. 
// Los celulares (Android/iOS) bloquean las peticiones HTTPS locales porque el certificado de desarrollo de tu PC no es válido para ellos.
// Para probar en la red local, usamos HTTP plano.
// app.UseHttpsRedirection(); 

app.UseAuthorization();
app.MapControllers();

// 3. Configuración de Red: 0.0.0.0 significa "Escuchar en todas mis IPs (LAN y Localhost)"
// El puerto 5000 es el estándar de HTTP para .NET.
app.Run("http://0.0.0.0:5000");

using Application.PaymentMethods.Dtos;
using Application.PaymentMethods.Interfaces;
using Application.Products.Interfaces;
using Application.Sales.Dtos;
using Application.Sales.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Helpers;
using WinForms.PopUps;

namespace WinForms
{
    public partial class UserControlNewSales : UserControl, IMessageFilter
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly ISuspendedSaleService _suspendedSaleService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private readonly TicketPrinter _ticketPrinter;

        private BindingList<SaleItemViewModel> _cartItems;
        private List<PaymentMethodGridDto> _paymentMethods = new List<PaymentMethodGridDto>();

        // Venta que se está mostrando en pantalla. Las demás quedan "en espera" en el servicio.
        private SuspendedSaleDto? _activeSale;

        // Mientras se cambia de pestaña, los handlers de la UI (método de pago, efectivo) no
        // tienen que guardar nada: estaríamos pisando la venta destino con datos de la anterior.
        private bool _isLoadingSale = false;

        private int? _lastSaleId = null;
        private decimal _currentTotal = 0;
        private const string CODIGO_VARIOS = "VARIOS";

        // Botones de cada fila del carrito
        private const string COL_MINUS = "colMinus";
        private const string COL_PLUS = "colPlus";
        private const string COL_REMOVE = "colRemove";
        private static readonly Font RowButtonFont = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);

        // Edición de la cantidad en la grilla: mientras se escribe, el total se calcula con este
        // valor "en vista previa" sin tocar el producto. Se aplica de verdad al confirmar la celda,
        // así un Esc deja todo como estaba.
        private const string COL_QUANTITY = "Quantity";
        private SaleItemViewModel? _qtyEditItem;
        private int? _qtyEditPreview;

        // Atajos de cobro (se leen con un IMessageFilter para que anden con el foco en cualquier
        // control de la pantalla, incluso escribiendo en el código de barras o el efectivo).
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const int VK_CONTROL = 0x11;
        private const int VK_ADD = 0x6B; // "+" del teclado numérico
        private const int VK_SUBTRACT = 0x6D; // "-" del teclado numérico
        private const long KEY_EXTENDED = 0x01000000; // bit 24 de lParam: Ctrl DERECHO
        private const long KEY_REPEAT = 0x40000000;   // bit 30 de lParam: tecla mantenida

        // Ctrl derecho se cobra al SOLTARLO y solo si no se apretó otra tecla en el medio:
        // así un Ctrl+C / Ctrl+V con el Ctrl derecho nunca emite una factura.
        private bool _rightCtrlArmed = false;
        private bool _messageFilterAdded = false;

        public UserControlNewSales(
            ISaleService saleService,
            IProductService productService,
            IPaymentMethodService paymentMethodService,
            ISuspendedSaleService suspendedSaleService,
            IServiceProvider serviceProvider,
            IConfiguration config,
            TicketPrinter ticketPrinter)
        {
            InitializeComponent();
            _saleService = saleService;
            _productService = productService;
            _paymentMethodService = paymentMethodService;
            _suspendedSaleService = suspendedSaleService;
            _serviceProvider = serviceProvider;
            _config = config;
            _ticketPrinter = ticketPrinter;

            _cartItems = new BindingList<SaleItemViewModel>();
        }

        private async void UserControlNewSales_Load(object sender, EventArgs e)
        {
            try
            {
                dgvSaleProducts.DataSource = _cartItems;

                ConfigGrid();

                // Los métodos de pago tienen que estar cargados antes de restaurar el que tenía
                // seleccionado la venta en espera.
                await LoadPaymentMethods();

                // Se recuperan las ventas que quedaron en espera (de navegar a otra pantalla, o
                // incluso de la última vez que se abrió el sistema), volviendo a la que la cajera
                // estaba usando y no a la primera.
                var abiertas = _suspendedSaleService.GetAll();
                var previa = abiertas.FirstOrDefault(s => s.Id == _suspendedSaleService.ActiveSaleId);

                LoadSaleIntoUi(previa ?? (abiertas.Count > 0 ? abiertas[0] : _suspendedSaleService.CreateNew()));
                RefreshSaleTabs();

                txtProduct.Focus();

                if (btnShowLastSale != null) btnShowLastSale.Enabled = false;

                System.Windows.Forms.Application.AddMessageFilter(this);
                _messageFilterAdded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar pantalla de ventas: " + ex.Message);
            }
        }

        private async Task LoadPaymentMethods()
        {
            var methods = await _paymentMethodService.GetAllForGridAsync();
            _paymentMethods = methods.ToList();

            cbxPaymentMethod.DataSource = _paymentMethods;
            cbxPaymentMethod.DisplayMember = "Name";
            cbxPaymentMethod.ValueMember = "Id";

            var efectivo = _paymentMethods.FirstOrDefault(m => m.Name.Trim().Equals("Efectivo", StringComparison.OrdinalIgnoreCase));

            if (efectivo != null)
            {
                cbxPaymentMethod.SelectedValue = efectivo.Id;
            }
            else if (_paymentMethods.Count > 0)
            {
                // Si no hay efectivo, seleccionamos el primero de la lista
                cbxPaymentMethod.SelectedIndex = 0;
            }
        }

        private async void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string input = txtProduct.Text.Trim();

                if (!string.IsNullOrEmpty(input))
                {
                    string code = input;
                    int quantity = 1;

                    //Si el usuario escribió "CANTIDAD * CODIGO" (ej: 6*7791234)
                    if (input.Contains("*"))
                    {
                        var parts = input.Split('*');
                        if (parts.Length == 2 && int.TryParse(parts[0], out int parsedQty))
                        {
                            quantity = parsedQty;
                            code = parts[1].Trim();
                        }
                    }

                    if (quantity > 0 && !string.IsNullOrEmpty(code))
                    {
                        await AddProducts(code, quantity);
                        txtProduct.Text = "";
                        txtProduct.Focus();
                    }
                }
            }
        }

        private async Task AddProducts(string code, int quantityToAdd)
        {
            try
            {
                var product = await _productService.GetProductForEditAsync(code);

                if (product == null)
                {
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!product.IsActive)
                {
                    MessageBox.Show($"El producto '{product.Name}' está inactivo.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Validación de Stock Acumulado
                var existingItem = _cartItems.FirstOrDefault(x => x.ProductCode == product.Code);
                int currentQtyInCart = existingItem?.Quantity ?? 0;

                //Lo que ya tengo en carrito + Lo que quiero agregar
                bool faltaStock = false;
                if (product.Code != CODIGO_VARIOS)
                {
                    if (currentQtyInCart + quantityToAdd > product.Stock)
                    {
                        faltaStock = true;
                    }
                }

                // Pregunta de Seguridad
                if (faltaStock)
                {
                    var result = MessageBox.Show(
                        $"Stock insuficiente para '{product.Name}'.\n" +
                        $"Stock Real: {product.Stock}\n" +
                        $"En Carrito: {currentQtyInCart}\n" +
                        $"Intentando agregar: {quantityToAdd}\n\n" +
                        "¿Desea venderlo de todas formas? (El stock quedará en 0)",
                        "Falta de Stock",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.No) return;
                }

                //Agregar o Actualizar
                if (existingItem != null)
                {
                    existingItem.Quantity += quantityToAdd;
                    _cartItems.ResetItem(_cartItems.IndexOf(existingItem));
                }
                else
                {
                    _cartItems.Add(new SaleItemViewModel
                    {
                        ProductCode = product.Code,
                        ProductName = product.Name,
                        UnitPrice = product.SalePrice,
                        Quantity = quantityToAdd
                    });
                }

                if (lblLastProduct != null)
                {
                    string lastProduct = $"{product.Name} (x{quantityToAdd})".ToUpper();
                    lblLastProduct.Text = lastProduct;
                    lblLastProduct.StateCommon.ShortText.Color1 = System.Drawing.Color.Green;
                }

                UpdateCalcs();
                PersistActiveSaleAndRefreshTabs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar producto: " + ex.Message);
            }
        }

        private void UpdateCalcs()
        {
            decimal subtotal = _cartItems.Sum(x => SubtotalWithPreview(x));

            if (lblSubTotal != null) lblSubTotal.Text = $"SUBTOTAL\n {subtotal.ToString("C2")}";

            decimal totalFinal = subtotal;

            if (cbxPaymentMethod.SelectedItem is PaymentMethodGridDto metodo)
            {
                decimal factor = 1 + ((metodo.Recharge - metodo.Discount) / 100m);
                totalFinal = subtotal * factor;
                /*
                if (lblRecargoDescuento != null)
                {
                    if (metodo.Recharge > 0)
                        lblRecargoDescuento.Text = $"Recargo: {metodo.Recharge}%";
                    else if (metodo.Discount > 0)
                        lblRecargoDescuento.Text = $"Descuento: {metodo.Discount}%";
                    else
                        lblRecargoDescuento.Text = "-";
                }*/
            }

            _currentTotal = totalFinal;

            lblTotal.Text = $"TOTAL A PAGAR\n {totalFinal.ToString("C0")}";
            GetChange(totalFinal);
        }

        private void GetChange(decimal total)
        {
            if (txtCash == null || lblChange == null || !txtCash.Visible) return;

            string cashText = txtCash.Text.Replace("$", "").Trim();

            if (decimal.TryParse(cashText, out decimal cash))
            {
                decimal change = cash - total;

                if (cash >= total)
                {
                    lblChange.Text = $"VUELTO\n$ {change.ToString("N2")}";
                    lblChange.StateCommon.ShortText.Color1 = System.Drawing.Color.Red;
                }
                else
                {
                    lblChange.Text = $"NO ALCANZA";
                    lblChange.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
                }
            }
            else
            {
                lblChange.Text = $"VUELTO\n$ 0.00";
                lblChange.StateCommon.ShortText.Color1 = System.Drawing.Color.Black;
            }
        }

        private async void btnSaleTicket_Click(object sender, EventArgs e)
        {
            await MakeSale(true, false);
        }

        private async void btnOnlySale_Click(object sender, EventArgs e)
        {
            await MakeSale(false, false);
        }

        private void PrintLocalTicket(string medioPago, decimal total, decimal pagaCon, string? note = null)
        {
            var itemsTicket = _cartItems.Select(x => new TicketItem
            {
                ProductName = x.ProductName,
                Quantity = x.Quantity,
                Subtotal = x.Subtotal
            }).ToList();

            string nombreImpresora = _config["Arca:PrinterName"] ?? "Microsoft Print to PDF";
            _ticketPrinter.PrintTicket(_lastSaleId ?? 0, DateTime.Now, itemsTicket, total, medioPago, pagaCon, pagaCon - total, nombreImpresora, note);
        }

        private void ConfigGrid()
        {
            dgvSaleProducts.AllowUserToAddRows = false;
            dgvSaleProducts.AllowUserToDeleteRows = false;
            dgvSaleProducts.RowHeadersVisible = false;
            dgvSaleProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSaleProducts.ReadOnly = true;

            if (dgvSaleProducts.Columns["ProductCode"] != null)
            {
                dgvSaleProducts.Columns["ProductCode"].HeaderText = "Cód.";
                dgvSaleProducts.Columns["ProductCode"].Width = 80;
            }

            if (dgvSaleProducts.Columns["ProductName"] != null)
            {
                dgvSaleProducts.Columns["ProductName"].HeaderText = "Producto";
                dgvSaleProducts.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvSaleProducts.Columns["UnitPrice"] != null)
            {
                dgvSaleProducts.Columns["UnitPrice"].HeaderText = "Precio";
                dgvSaleProducts.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
                dgvSaleProducts.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSaleProducts.Columns["UnitPrice"].Width = 100;
            }

            if (dgvSaleProducts.Columns["Quantity"] != null)
            {
                dgvSaleProducts.Columns["Quantity"].HeaderText = "Cant.";
                dgvSaleProducts.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSaleProducts.Columns["Quantity"].Width = 60;
            }

            if (dgvSaleProducts.Columns["Subtotal"] != null)
            {
                dgvSaleProducts.Columns["Subtotal"].HeaderText = "Subtotal";
                dgvSaleProducts.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
                dgvSaleProducts.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSaleProducts.Columns["Subtotal"].Width = 100;
            }

            dgvSaleProducts.RowHeadersVisible = false;
            dgvSaleProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSaleProducts.ReadOnly = true;

            // Solo la cantidad es editable, y solo al hacerle click (EditProgrammatically): si se
            // pudiera editar tecleando, la pistola escaneando con el foco en la grilla escribiría
            // el código de barras como cantidad.
            dgvSaleProducts.ReadOnly = false;
            dgvSaleProducts.EditMode = DataGridViewEditMode.EditProgrammatically;
            foreach (DataGridViewColumn col in dgvSaleProducts.Columns)
                col.ReadOnly = col.Name != COL_QUANTITY;

            // Botones por fila: [-] Cant. [+] a los lados de la cantidad, y [✕] al final.
            if (dgvSaleProducts.Columns[COL_MINUS] == null)
            {
                dgvSaleProducts.Columns.Add(CreateRowButtonColumn(COL_MINUS, "−"));
                dgvSaleProducts.Columns.Add(CreateRowButtonColumn(COL_PLUS, "+"));
                dgvSaleProducts.Columns.Add(CreateRowButtonColumn(COL_REMOVE, "✕"));
            }

            string[] orden = { "ProductCode", "ProductName", "UnitPrice", COL_MINUS, "Quantity", COL_PLUS, "Subtotal", COL_REMOVE };
            int displayIndex = 0;
            foreach (string nombre in orden)
            {
                if (dgvSaleProducts.Columns[nombre] != null)
                    dgvSaleProducts.Columns[nombre].DisplayIndex = displayIndex++;
            }
        }

        private static DataGridViewButtonColumn CreateRowButtonColumn(string name, string text)
        {
            return new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = "",
                Text = text,
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 46,
                Resizable = DataGridViewTriState.False
            };
        }

        private void dgvSaleProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.CellStyle == null) return;

            // Subtotal de la fila cuya cantidad se está escribiendo: muestra la cuenta en vivo.
            if (dgvSaleProducts.Columns[e.ColumnIndex].Name == "Subtotal"
                && _qtyEditPreview.HasValue
                && e.RowIndex >= 0
                && dgvSaleProducts.Rows[e.RowIndex].DataBoundItem == _qtyEditItem)
            {
                e.Value = SubtotalWithPreview(_qtyEditItem!);
                return;
            }

            // Se pinta acá y no en el estilo de la columna: el estilo de filas alternas de la
            // grilla tiene prioridad sobre el de la columna y les pisaría el color.
            Color? back = dgvSaleProducts.Columns[e.ColumnIndex].Name switch
            {
                COL_MINUS => Color.FromArgb(230, 233, 240),
                COL_PLUS => Color.FromArgb(178, 233, 178),
                COL_REMOVE => Color.FromArgb(255, 196, 196),
                _ => null
            };

            if (back == null) return;

            e.CellStyle.BackColor = back.Value;
            e.CellStyle.SelectionBackColor = back.Value;
            e.CellStyle.ForeColor = Color.FromArgb(20, 25, 70);
            e.CellStyle.SelectionForeColor = Color.FromArgb(20, 25, 70);
            e.CellStyle.Font = RowButtonFont;
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvSaleProducts_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvSaleProducts.Rows[e.RowIndex].DataBoundItem is not SaleItemViewModel item) return;

            string columna = dgvSaleProducts.Columns[e.ColumnIndex].Name;

            // BeginInvoke: modificar la lista enlazada dentro del propio evento de la grilla puede
            // tirar "reentrant call to SetCurrentCellAddressCore"; se hace apenas termina el click.
            switch (columna)
            {
                case COL_PLUS:
                    BeginInvoke(new Action(async () =>
                    {
                        // Mismo camino que escanear el producto otra vez: respeta el aviso de stock.
                        await AddProducts(item.ProductCode, 1);
                        txtProduct.Focus();
                    }));
                    break;

                case COL_MINUS:
                    BeginInvoke(new Action(() => DecreaseQuantity(item)));
                    break;

                case COL_REMOVE:
                    BeginInvoke(new Action(() => RemoveItem(item)));
                    break;
            }
        }

        #region Edición de cantidad en la grilla

        private decimal SubtotalWithPreview(SaleItemViewModel item)
        {
            return item == _qtyEditItem && _qtyEditPreview.HasValue
                ? item.UnitPrice * _qtyEditPreview.Value
                : item.Subtotal;
        }

        private void dgvSaleProducts_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvSaleProducts.Columns[e.ColumnIndex].Name != COL_QUANTITY) return;

            dgvSaleProducts.CurrentCell = dgvSaleProducts[e.ColumnIndex, e.RowIndex];
            dgvSaleProducts.BeginEdit(true); // true = selecciona el número para pisarlo directo
        }

        private void dgvSaleProducts_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            _qtyEditItem = dgvSaleProducts.Rows[e.RowIndex].DataBoundItem as SaleItemViewModel;
            _qtyEditPreview = null;
        }

        private void dgvSaleProducts_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is not TextBox tb) return;

            // El control de edición se reutiliza entre celdas: se desengancha antes de enganchar
            // para no sumar handlers duplicados.
            tb.KeyPress -= QuantityEditor_KeyPress;
            tb.TextChanged -= QuantityEditor_TextChanged;
            tb.KeyPress += QuantityEditor_KeyPress;
            tb.TextChanged += QuantityEditor_TextChanged;
            tb.MaxLength = 5;
        }

        private void QuantityEditor_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void QuantityEditor_TextChanged(object? sender, EventArgs e)
        {
            if (_qtyEditItem == null || sender is not TextBox tb) return;

            // Vacío, 0 o algo pegado que no es número: se calcula con la cantidad que ya tenía.
            _qtyEditPreview = int.TryParse(tb.Text, out int qty) && qty > 0 ? qty : null;

            UpdateCalcs();
            if (dgvSaleProducts.CurrentCell != null && dgvSaleProducts.Columns["Subtotal"] != null)
                dgvSaleProducts.InvalidateCell(dgvSaleProducts.Columns["Subtotal"].Index, dgvSaleProducts.CurrentCell.RowIndex);
        }

        private void dgvSaleProducts_CellParsing(object? sender, DataGridViewCellParsingEventArgs e)
        {
            if (dgvSaleProducts.Columns[e.ColumnIndex].Name != COL_QUANTITY) return;

            e.Value = int.TryParse(e.Value?.ToString(), out int qty) && qty > 0
                ? qty
                : (_qtyEditItem?.Quantity ?? 1); // Valor inválido: queda la cantidad anterior
            e.ParsingApplied = true;
        }

        private void dgvSaleProducts_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            var item = _qtyEditItem;
            _qtyEditItem = null;
            _qtyEditPreview = null;

            // Sincrónico: si la edición terminó porque se cobró por atajo, el total ya tiene que
            // estar actualizado cuando MakeSale lo lea.
            UpdateCalcs();
            PersistActiveSale();

            BeginInvoke(new Action(() =>
            {
                if (item != null)
                {
                    int index = _cartItems.IndexOf(item);
                    if (index >= 0) _cartItems.ResetItem(index); // refresca el Subtotal de la fila
                }

                // Las pestañas se recrean enteras: si la edición terminó porque se tocó una
                // pestaña, recrearlas ahora se comería ese click. En ese caso la pestaña tocada
                // ya las refresca sola.
                if (!flpSaleTabs.ContainsFocus) RefreshSaleTabs();

                // Si el foco quedó en la grilla (Enter), vuelve al código de barras para seguir
                // escaneando. Si se hizo click en otro control, no se lo saca.
                if (dgvSaleProducts.ContainsFocus) txtProduct.Focus();
            }));
        }

        #endregion

        private void DecreaseQuantity(SaleItemViewModel item)
        {
            int index = _cartItems.IndexOf(item);
            if (index < 0) return;

            // El mínimo es 1: para sacar el producto entero está el botón ✕ (o Supr).
            if (item.Quantity > 1)
            {
                item.Quantity--;
                _cartItems.ResetItem(index);
                UpdateCalcs();
                PersistActiveSaleAndRefreshTabs();
            }

            txtProduct.Focus();
        }

        private void dgvSaleProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) DeleteSelected();
        }

        private void DeleteSelected()
        {
            if (dgvSaleProducts.CurrentRow?.DataBoundItem is SaleItemViewModel item)
            {
                RemoveItem(item);
            }
        }

        private void RemoveItem(SaleItemViewModel item)
        {
            if (!_cartItems.Remove(item)) return;

            UpdateCalcs();
            PersistActiveSaleAndRefreshTabs();
            txtProduct.Focus();
        }

        #region Atajos de cobro

        /// <summary>
        /// "+" del teclado numérico = VENTA + TICKET (el botón está oculto, solo anda por atajo).
        /// "-" del teclado numérico = SOLO VENTA, sin ticket ni factura (botón oculto, solo atajo).
        /// Ctrl DERECHO solo = FACTURAR.
        /// </summary>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != WM_KEYDOWN && m.Msg != WM_SYSKEYDOWN && m.Msg != WM_KEYUP && m.Msg != WM_SYSKEYUP)
                return false;

            if (!ShortcutsActive())
            {
                _rightCtrlArmed = false;
                return false;
            }

            int vk = m.WParam.ToInt32();
            long lParam = m.LParam.ToInt64();
            bool isRightCtrl = vk == VK_CONTROL && (lParam & KEY_EXTENDED) != 0;
            bool isKeyDown = m.Msg == WM_KEYDOWN || m.Msg == WM_SYSKEYDOWN;

            if (isKeyDown)
            {
                if (isRightCtrl)
                {
                    if ((lParam & KEY_REPEAT) == 0) _rightCtrlArmed = true;
                    return false;
                }

                // Cualquier otra tecla mientras se mantiene el Ctrl derecho anula el atajo.
                _rightCtrlArmed = false;

                if (vk == VK_ADD)
                {
                    if ((lParam & KEY_REPEAT) == 0)
                        BeginInvoke(new Action(() => SaleTicketShortcut()));

                    return true; // Se consume: el "+" no se escribe en ningún campo.
                }

                if (vk == VK_SUBTRACT)
                {
                    if ((lParam & KEY_REPEAT) == 0)
                        BeginInvoke(new Action(() => OnlySaleShortcut()));

                    return true; // Se consume: el "-" no se escribe en ningún campo.
                }

                return false;
            }

            // Key up
            if (isRightCtrl && _rightCtrlArmed)
            {
                _rightCtrlArmed = false;
                BeginInvoke(new Action(() => SaleAfipShortcut()));
            }

            return false;
        }

        /// <summary>
        /// Solo con esta pantalla visible y su ventana activa. Con un MessageBox o un popup
        /// abierto la ventana activa es otra, así que los atajos no hacen nada.
        /// </summary>
        private bool ShortcutsActive()
        {
            if (IsDisposed || !IsHandleCreated || !Visible) return false;

            var form = FindForm();
            return form != null && Form.ActiveForm == form;
        }

        private void SaleTicketShortcut()
        {
            // Enabled = false mientras hay un cobro en curso: evita cobrar dos veces.
            if (!ShortcutsActive() || !btnSaleTicket.Enabled) return;
            CommitQuantityEdit();
            btnSaleTicket_Click(btnSaleTicket, EventArgs.Empty);
        }

        private void OnlySaleShortcut()
        {
            if (!ShortcutsActive() || !btnOnlySale.Enabled) return;
            CommitQuantityEdit();
            btnOnlySale_Click(btnOnlySale, EventArgs.Empty);
        }

        private void SaleAfipShortcut()
        {
            if (!ShortcutsActive() || !btnSaleAFIP.Enabled) return;
            CommitQuantityEdit();
            btnSaleAFIP_Click(btnSaleAFIP, EventArgs.Empty);
        }

        /// <summary>
        /// Con un botón el foco sale de la grilla y la cantidad se confirma sola; con un atajo
        /// no, así que se confirma a mano para no cobrar con la cantidad vieja.
        /// </summary>
        private void CommitQuantityEdit()
        {
            if (dgvSaleProducts.IsCurrentCellInEditMode) dgvSaleProducts.EndEdit();
        }

        #endregion

        private void cbxPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckPaymentMethodVisibility();
            UpdateCalcs();
            PersistActiveSale();
        }

        private void CheckPaymentMethodVisibility()
        {
            if (cbxPaymentMethod.SelectedItem is PaymentMethodGridDto method)
            {
                bool isCash = method.Name.Trim().Equals("Efectivo", StringComparison.OrdinalIgnoreCase);

                if (txtCash != null)
                {
                    txtCash.Visible = isCash;
                    if (!isCash) txtCash.Text = "";
                }

                if (lblChange != null) lblChange.Visible = isCash;
            }
        }

        #region Ventas en espera (pestañas)

        /// <summary>
        /// Vuelca lo que hay en pantalla (productos, método de pago, efectivo) a la venta activa
        /// y lo persiste. Se llama en cada cambio para que un corte de luz no se lleve nada.
        /// </summary>
        private void PersistActiveSale()
        {
            if (_activeSale == null || _isLoadingSale) return;

            _activeSale.Items = _cartItems.Select(x => new SuspendedSaleItemDto
            {
                ProductCode = x.ProductCode,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity
            }).ToList();

            _activeSale.PaymentMethodId = cbxPaymentMethod.SelectedValue as int?;
            _activeSale.CashText = txtCash?.Text ?? string.Empty;

            _suspendedSaleService.Save(_activeSale);
        }

        /// <summary>Persiste y además refresca los rótulos de las pestañas (cambió el total).</summary>
        private void PersistActiveSaleAndRefreshTabs()
        {
            if (_isLoadingSale) return;

            PersistActiveSale();
            RefreshSaleTabs();
        }

        private void LoadSaleIntoUi(SuspendedSaleDto sale)
        {
            _isLoadingSale = true;
            try
            {
                _activeSale = sale;
                _suspendedSaleService.ActiveSaleId = sale.Id;

                _cartItems.Clear();
                foreach (var item in sale.Items)
                {
                    _cartItems.Add(new SaleItemViewModel
                    {
                        ProductCode = item.ProductCode,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity
                    });
                }

                // Solo se restaura el método de pago si todavía existe (lo pudieron dar de baja).
                if (sale.PaymentMethodId.HasValue && _paymentMethods.Any(m => m.Id == sale.PaymentMethodId.Value))
                {
                    cbxPaymentMethod.SelectedValue = sale.PaymentMethodId.Value;
                }

                if (txtCash != null) txtCash.Text = sale.CashText ?? string.Empty;
            }
            finally
            {
                _isLoadingSale = false;
            }

            CheckPaymentMethodVisibility();
            UpdateCalcs();
            UpdateLastProductLabel();
        }

        /// <summary>
        /// Deja el cartel mostrando el último producto de la venta que está en pantalla, como si
        /// recién se hubiera escaneado. Al cambiar de pestaña sirve para retomar el hilo de esa
        /// venta; si la venta está vacía, vuelve al mensaje neutro.
        /// Siempre se deriva del carrito actual, así que nunca queda colgado el producto de otra venta.
        /// </summary>
        private void UpdateLastProductLabel()
        {
            if (lblLastProduct == null) return;

            var ultimo = _cartItems.LastOrDefault();

            if (ultimo == null)
            {
                lblLastProduct.Text = "Listo para vender";
                lblLastProduct.StateCommon.ShortText.Color1 = Color.Empty; // Color.Empty = vuelve al de la paleta
                return;
            }

            lblLastProduct.Text = $"{ultimo.ProductName} (x{ultimo.Quantity})".ToUpper();
            lblLastProduct.StateCommon.ShortText.Color1 = Color.Green;
        }

        private void SwitchToSale(int saleId)
        {
            if (_activeSale != null && _activeSale.Id == saleId) return;

            PersistActiveSale();

            var destino = _suspendedSaleService.GetAll().FirstOrDefault(s => s.Id == saleId);
            if (destino == null) return;

            LoadSaleIntoUi(destino);
            RefreshSaleTabs();
            txtProduct.Focus();
        }

        /// <summary>
        /// Pasa a la venta que corresponda después de cerrar la activa (por cobro o por descarte).
        /// Siempre queda al menos una venta abierta, lista para el próximo cliente.
        /// </summary>
        private void ActivateAnotherSale()
        {
            var restantes = _suspendedSaleService.GetAll();
            LoadSaleIntoUi(restantes.Count > 0 ? restantes[0] : _suspendedSaleService.CreateNew());
            RefreshSaleTabs();
        }

        private void RefreshSaleTabs()
        {
            // Los botones se recrean enteros: son pocos y así no hay que sincronizar estados.
            var anteriores = flpSaleTabs.Controls.Cast<Control>().ToList();
            flpSaleTabs.Controls.Clear();
            foreach (var c in anteriores) c.Dispose();

            var abiertas = _suspendedSaleService.GetAll();

            for (int i = 0; i < abiertas.Count; i++)
            {
                var sale = abiertas[i];
                bool esActiva = _activeSale != null && sale.Id == _activeSale.Id;

                // El rótulo es la POSICIÓN, no el Id: así al cerrar una venta las que quedan se
                // renumeran solas y la barra siempre se lee "Venta 1, Venta 2, Venta 3".
                int posicion = i + 1;

                var btn = new Krypton.Toolkit.KryptonButton
                {
                    Size = new Size(210, 32),
                    Margin = new Padding(2, 0, 2, 0),
                    Tag = sale.Id
                };
                btn.Values.Text = $"Venta {posicion}  ·  {sale.SubTotal.ToString("C0")}  ·  {sale.CreatedAt:HH:mm}";
                btn.Values.DropDownArrowColor = Color.Empty;
                btn.StateCommon.Back.Color1 = esActiva
                    ? Color.FromArgb(160, 205, 254)
                    : Color.FromArgb(230, 233, 240);
                btn.StateCommon.Content.ShortText.Font = new Font(
                    "Microsoft Sans Serif", 9.75F,
                    esActiva ? FontStyle.Bold : FontStyle.Regular,
                    GraphicsUnit.Point, 0);

                int saleId = sale.Id;
                btn.Click += (s, e) => SwitchToSale(saleId);

                flpSaleTabs.Controls.Add(btn);
            }

            var btnNueva = new Krypton.Toolkit.KryptonButton
            {
                Size = new Size(120, 32),
                Margin = new Padding(10, 0, 2, 0)
            };
            btnNueva.Values.Text = "+ NUEVA";
            btnNueva.Values.DropDownArrowColor = Color.Empty;
            btnNueva.StateCommon.Back.Color1 = Color.FromArgb(178, 233, 178);
            btnNueva.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNueva.Click += btnNewSaleTab_Click;
            flpSaleTabs.Controls.Add(btnNueva);

            var btnCerrar = new Krypton.Toolkit.KryptonButton
            {
                Size = new Size(48, 32),
                Margin = new Padding(2, 0, 2, 0)
            };
            btnCerrar.Values.Text = "✕";
            btnCerrar.Values.DropDownArrowColor = Color.Empty;
            btnCerrar.StateCommon.Back.Color1 = Color.FromArgb(255, 196, 196);
            btnCerrar.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrar.Click += btnCloseSaleTab_Click;
            flpSaleTabs.Controls.Add(btnCerrar);
        }

        private void btnNewSaleTab_Click(object? sender, EventArgs e)
        {
            if (_suspendedSaleService.GetAll().Count >= _suspendedSaleService.MaxOpenSales)
            {
                MessageBox.Show(
                    $"Ya hay {_suspendedSaleService.MaxOpenSales} ventas abiertas. Cobrá o cerrá alguna antes de abrir otra.",
                    "Demasiadas ventas abiertas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PersistActiveSale();
            LoadSaleIntoUi(_suspendedSaleService.CreateNew());
            RefreshSaleTabs();
            txtProduct.Focus();
        }

        private void btnCloseSaleTab_Click(object? sender, EventArgs e)
        {
            if (_activeSale == null) return;

            if (_cartItems.Count > 0)
            {
                int posicion = _suspendedSaleService.GetAll().ToList().FindIndex(s => s.Id == _activeSale.Id) + 1;

                var confirmar = MessageBox.Show(
                    $"La Venta {posicion} tiene {_cartItems.Count} producto(s) cargados.\n\n¿Descartarla?",
                    "Cerrar venta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (confirmar == DialogResult.No) return;
            }

            _suspendedSaleService.Remove(_activeSale.Id);
            ActivateAnotherSale();
            txtProduct.Focus();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Red de seguridad: al navegar a otra pantalla este control se destruye. Ya se guarda
            // en cada cambio, pero un último volcado no cuesta nada.
            try { PersistActiveSale(); } catch { }

            if (_messageFilterAdded)
            {
                System.Windows.Forms.Application.RemoveMessageFilter(this);
                _messageFilterAdded = false;
            }

            base.OnHandleDestroyed(e);
        }

        #endregion

        private async Task MakeSale(bool ticket, bool isFiscal)
        {
            if (_cartItems.Count == 0)
            {
                MessageBox.Show("No hay productos leídos.");
                txtProduct.Focus();
                return;
            }

            if (cbxPaymentMethod.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un método de pago.");
                return;
            }

            btnSaleTicket.Enabled = false;
            btnOnlySale.Enabled = false;
            if (btnSale != null) btnSale.Enabled = false;
            if (btnSaleAFIP != null) btnSaleAFIP.Enabled = false;

            try
            {
                var paymentMethod = (PaymentMethodGridDto)cbxPaymentMethod.SelectedItem;

                var saleDto = new CreateSaleDto
                {
                    PaymentMethodId = paymentMethod.Id,
                    Items = _cartItems.Select(x => new SaleDetailDto
                    {
                        ProductCode = x.ProductCode,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice
                    }).ToList(),
                    IsFiscal = isFiscal
                };

                SaleResultDto result = await _saleService.CreateSaleAsync(saleDto);

                _lastSaleId = result.SaleId;
                if (btnShowLastSale != null) btnShowLastSale.Enabled = true;
                decimal totalImpresion = _currentTotal;

                decimal paysWith = 0;
                if (txtCash.Visible)
                    decimal.TryParse(txtCash.Text.Replace("$", "").Trim(), out paysWith);

                // Print
                if (ticket)
                {
                    if (isFiscal && result.FiscalData != null)
                    {
                        long.TryParse(result.FiscalData.IssuerCuit ?? _config["Arca:Cuit"], out long cuitEmisor);

                        var datosQr = new AfipQR
                        {
                            fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                            cuit = cuitEmisor,
                            ptoVta = result.FiscalData.PointOfSale, //
                            tipoCmp = result.FiscalData.InvoiceType, //
                            nroCmp = (int)result.FiscalData.InvoiceNumber, //
                            importe = _currentTotal, // Importe total de la venta
                            moneda = "PES",
                            ctz = 1,
                            // Si CustomerDocType es nulo (consumidor final sin ident.), usa 99 y doc 0
                            tipoDocRec = int.Parse(result.FiscalData.CustomerDocType ?? "99"),
                            nroDocRec = long.Parse(result.FiscalData.CustomerDocNumber ?? "0"),
                            tipoCodAut = "E",
                            codAut = long.Parse(result.FiscalData.CAE)
                        };

                        // Lista de items para el ticket
                        var itemsTicket = _cartItems.Select(x => new TicketItem
                        {
                            ProductName = x.ProductName,
                            Quantity = x.Quantity,
                            Subtotal = x.Subtotal
                        }).ToList();


                        string nombreImpresora = _config["Arca:PrinterName"] ?? "Microsoft Print to PDF";
                        // Imprimimos pasando los datos QR y la lista visual
                        _ticketPrinter.ImprimirFactura(
                            datosQr,
                            result.FiscalData,
                            "CONSUMIDOR FINAL",
                            "0",
                            itemsTicket,
                            DateTime.Now,
                            nombreImpresora
                        );
                    }
                    else if (isFiscal)
                    {
                        // ARCA no respondió a tiempo: la venta ya está guardada, se imprime un
                        // comprobante provisorio y la factura real queda pendiente en la cola.
                        PrintLocalTicket(paymentMethod.Name, totalImpresion, paysWith,
                            "COMPROBANTE PROVISORIO - Factura en trámite, se emitirá con CAE apenas ARCA esté disponible.");
                    }
                    else
                    {
                        // Ticket no fiscal
                        PrintLocalTicket(paymentMethod.Name, totalImpresion, paysWith);
                    }
                }

                // La venta se concretó: su pestaña deja de existir y se pasa a otra abierta (o a
                // una vacía si era la última). LoadSaleIntoUi limpia la pantalla.
                if (_activeSale != null) _suspendedSaleService.Remove(_activeSale.Id);
                _currentTotal = 0;

                // ActivateAnotherSale -> LoadSaleIntoUi ya actualiza el cartel del último producto.
                ActivateAnotherSale();

                string msg = ticket ? "¡Venta registrada e impresa!" : "¡Venta registrada!";
                MessageBox.Show(msg, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (isFiscal)
                {
                    if (result.FiscalData != null)
                    {
                        MessageBox.Show("¡Venta Fiscal Autorizada!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            "La venta se registró, pero ARCA no respondió a tiempo. La factura quedó pendiente y se va a emitir sola más tarde (o desde Ajustes > Facturación Pendiente).",
                            "Factura pendiente",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }


                txtProduct.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Cobrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSaleTicket.Enabled = true;
                btnOnlySale.Enabled = true;
                if (btnSaleAFIP != null) btnSaleAFIP.Enabled = true;
            }
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            UpdateCalcs();
            PersistActiveSale();
        }

        private void btnAddVarious_Click(object sender, EventArgs e)
        {
            string txtAmount = txtVariosAmount.Text.Trim();
            if (string.IsNullOrEmpty(txtAmount)) return;

            if (!decimal.TryParse(txtAmount, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Ingrese un monto válido mayor a 0.");
                return;
            }

            // Acumular
            var itemVarios = _cartItems.FirstOrDefault(x => x.ProductCode == CODIGO_VARIOS);

            if (itemVarios != null)
            {
                // Si ya existe, acumula
                itemVarios.UnitPrice += amount;
                _cartItems.ResetItem(_cartItems.IndexOf(itemVarios));
            }
            else
            {
                // Si no existe, se crea.
                _cartItems.Add(new SaleItemViewModel
                {
                    ProductCode = CODIGO_VARIOS,
                    ProductName = "Varios/Fiambrería",
                    Quantity = 1,
                    UnitPrice = amount
                });
            }

            UpdateCalcs();
            PersistActiveSaleAndRefreshTabs();
            txtVariosAmount.Text = "";
            txtProduct.Focus();
        }

        private void btnShowLastSale_Click(object sender, EventArgs e)
        {
            if (_lastSaleId.HasValue)
            {
                var formDetalle = _serviceProvider.GetRequiredService<FormSaleDetail>();

                formDetalle.LoadData(_lastSaleId.Value);
                formDetalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("No hay venta reciente en esta sesión de pantalla.");
            }
        }

        private async void btnSaleAFIP_Click(object sender, EventArgs e)
        {
            await MakeSale(true, true);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _cartItems.Clear();
            if (txtCash != null) txtCash.Text = "";
            UpdateLastProductLabel();
            _currentTotal = 0;

            UpdateCalcs();
            PersistActiveSaleAndRefreshTabs();
            txtProduct.Focus();
        }

        private void txtVariosAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAddVarious_Click(sender, e);
            }
        }
    }
}

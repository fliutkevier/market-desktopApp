using Application.Sales.Dtos;
using Application.Sales.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
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

namespace WinForms.PopUps
{
    public partial class FormSaleDetail : Form
    {
        private readonly ISaleService _saleService;
        private readonly IConfiguration _config;
        private readonly TicketPrinter _ticketPrinter;
        private int _saleId;

        // Para Imprimir
        private SaleGridDto? _saleHeader;
        private List<SaleDetailViewDto>? _saleDetails;
        private FiscalDocument? _fiscalDoc;

        public FormSaleDetail(ISaleService saleService, IConfiguration configuration, TicketPrinter ticketPrinter)
        {
            InitializeComponent();
            _saleService = saleService;
            _config = configuration;
            _ticketPrinter = ticketPrinter;
        }

        private void FormSaleDetail_Load(object sender, EventArgs e)
        {

        }

        public async void LoadData(int saleId)
        {
            _saleId = saleId;
            lblTitle.Text = $"Detalle de Venta #{_saleId}";

            try
            {
                _saleHeader = await _saleService.GetSaleHeaderAsync(_saleId);

                var saleDeatils = await _saleService.GetSaleDetailsAsync(_saleId);
                _saleDetails = saleDeatils.ToList();

                _fiscalDoc = await _saleService.GetFiscalDocumentBySaleIdAsync(_saleId);

                if (_saleHeader != null)
                {
                    lblSaleNumber.Text = $"Nro Venta\n#{_saleHeader.Id}";
                    lblSaleDate.Text = $"Fecha\n{_saleHeader.Date.ToString("dd/MM/yyyy HH:mm")}";
                    lblSaleUser.Text = $"Usuario\n{_saleHeader.User}";
                    lblPaymentMethod.Text = $"Método\n{_saleHeader.PaymentMethod}";
                    lblTotal.Text = $"Total\n{_saleHeader.Total.ToString("C2")}";
                }

                dgvProducts.DataSource = _saleDetails;
                ConfigGrid();

                chkAFIP.Checked = _fiscalDoc?.Status == FiscalDocumentStatus.Approved;
                chkAFIP.Enabled = false;
                chkAFIP.Text = _fiscalDoc == null
                    ? "No Facturado"
                    : _fiscalDoc.Status == FiscalDocumentStatus.Approved
                        ? "Facturado"
                        : "Pendiente (ARCA)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalles: " + ex.Message);
                this.Close();
            }
        }

        private void ConfigGrid()
        {
            dgvProducts.AutoGenerateColumns = true;

            dgvProducts.RowHeadersVisible = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.ReadOnly = true;

            if (dgvProducts.Columns["ProductCode"] != null)
            {
                dgvProducts.Columns["ProductCode"].HeaderText = "Cód.";
            }

            if (dgvProducts.Columns["ProductName"] != null)
            {
                dgvProducts.Columns["ProductName"].HeaderText = "Producto";
            }

            if (dgvProducts.Columns["Quantity"] != null)
            {
                dgvProducts.Columns["Quantity"].HeaderText = "Cant.";
            }

            if (dgvProducts.Columns["UnitPrice"] != null)
            {
                dgvProducts.Columns["UnitPrice"].HeaderText = "Precio Unit.";
                dgvProducts.Columns["UnitPrice"].DefaultCellStyle.Format = "C2";
            }

            if (dgvProducts.Columns["CostPrice"] != null)
            {
                dgvProducts.Columns["CostPrice"].HeaderText = "Costo Unit.";
                dgvProducts.Columns["CostPrice"].DefaultCellStyle.Format = "C2";
            }

            if (dgvProducts.Columns["SubTotal"] != null)
            {
                dgvProducts.Columns["SubTotal"].HeaderText = "Subtotal";
                dgvProducts.Columns["SubTotal"].DefaultCellStyle.Format = "C2";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrintTicket_Click(object sender, EventArgs e)
        {
            if (_saleHeader == null || _saleDetails == null) return;

            try
            {
                string nombreImpresora = _config["Arca:PrinterName"];

                var ticketItems = _saleDetails.Select(d => new TicketItem
                {
                    ProductName = d.ProductName,
                    Quantity = d.Quantity,
                    Subtotal = d.SubTotal
                }).ToList();

                decimal total = _saleHeader.Total;

                if (_fiscalDoc != null && _fiscalDoc.Status == FiscalDocumentStatus.Approved)
                {
                    // === IMPRESIÓN FISCAL ===

                    // El CUIT sale del comprobante, no de la configuración: una factura emitida
                    // por el contribuyente anterior se tiene que reimprimir con SU CUIT.
                    long.TryParse(_fiscalDoc.IssuerCuit ?? _config["Arca:Cuit"], out long cuitEmisor);

                    // Reconstruir objeto AfipQR
                    var datosQr = new AfipQR
                    {
                        fecha = _saleHeader.Date.ToString("yyyy-MM-dd"), // Fecha de la venta original
                        cuit = cuitEmisor,
                        ptoVta = _fiscalDoc.PointOfSale,
                        tipoCmp = _fiscalDoc.InvoiceType,
                        nroCmp = (int)(_fiscalDoc.InvoiceNumber ?? 0),
                        importe = _saleHeader.Total,
                        moneda = "PES",
                        ctz = 1,
                        tipoDocRec = int.Parse(_fiscalDoc.CustomerDocType ?? "99"),
                        nroDocRec = long.Parse(_fiscalDoc.CustomerDocNumber ?? "0"),
                        tipoCodAut = "E",
                        codAut = long.Parse(_fiscalDoc.CAE ?? "0")
                    };

                    _ticketPrinter.ImprimirFactura(
                        datosQr,
                        _fiscalDoc,
                        "CONSUMIDOR FINAL",
                        _fiscalDoc.CustomerDocNumber ?? "0",
                        ticketItems,
                        _saleHeader.Date,
                        nombreImpresora
                    );
                }
                else
                {
                    decimal paysWith = _saleHeader.Total;
                    decimal change = 0;
                    string? note = _fiscalDoc?.Status == FiscalDocumentStatus.Pending
                        ? "COMPROBANTE PROVISORIO - Factura en trámite, todavía no tiene CAE."
                        : null;

                    _ticketPrinter.PrintTicket(
                        _saleHeader.Id,
                        _saleHeader.Date,
                        ticketItems,
                        _saleHeader.Total,
                        _saleHeader.PaymentMethod,
                        paysWith,
                        change,
                        nombreImpresora,
                        note
                    );
                }

                MessageBox.Show("Ticket enviado a la impresora.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reimprimir: " + ex.Message);
            }
        }
    }
}

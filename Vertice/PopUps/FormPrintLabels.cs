using Application.Products.Dtos;
using Application.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.PopUps
{
    public partial class FormPrintLabels : Form
    {
        private readonly ILabelQueueService _labelQueueService;
        private BindingList<ProductLabelDto> _bindingList;

        private int _printIndex = 0;
        public FormPrintLabels(ILabelQueueService labelQueueService)
        {
            InitializeComponent();
            _labelQueueService = labelQueueService;
        }

        private void LoadGrid()
        {
            var list = _labelQueueService.GetQueue();
            _bindingList = new BindingList<ProductLabelDto>(list);

            dgvPrintProducts.DataSource = _bindingList;


            dgvPrintProducts.Columns["ProductCode"].ReadOnly = true;
            dgvPrintProducts.Columns["ProductCode"].HeaderText = "Código";
            dgvPrintProducts.Columns["PrintName"].HeaderText = "Nombre a Imprimir";
            dgvPrintProducts.Columns["Price"].HeaderText = "Precio";
            dgvPrintProducts.Columns["Price"].DefaultCellStyle.Format = "C2";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            _labelQueueService.ClearQueue();
            LoadGrid();
        }

        private void FormPrintLabels_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_bindingList.Count == 0)
            {
                MessageBox.Show("No hay etiquetas para imprimir.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo PDF|*.pdf";
            saveFileDialog.Title = "Guardar Etiquetas";
            saveFileDialog.FileName = $"Etiquetas_{DateTime.Now:HHmm}";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivo = saveFileDialog.FileName;

                _printIndex = 0;

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

                // Configuración para forzar la escritura en el archivo sin preguntar
                pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                pd.PrinterSettings.PrintToFile = true;
                pd.PrinterSettings.PrintFileName = rutaArchivo;

                pd.PrintPage += Pd_PrintPage;

                try
                {
                    pd.Print();

                    Process.Start(new ProcessStartInfo(rutaArchivo) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar o abrir el PDF: " + ex.Message);
                }
            }
            /*
            _printIndex = 0;

            PrintDocument pd = new PrintDocument();

            pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

            pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            pd.PrintPage += Pd_PrintPage;

            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message);
            }*/
        }

        private float MmToDisplay(float mm)
        {
            return (mm * 100f) / 25.4f;
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // --- MEDIDAS (2 Columnas x 5 Filas) ---
            float labelWidth = MmToDisplay(95);
            float labelHeight = MmToDisplay(55);

            float leftMargin = MmToDisplay(10);
            float topMargin = MmToDisplay(10);
            float padding = MmToDisplay(3);

            float nameTopPadding = MmToDisplay(4);

            int columnsPerPage = 2;
            int rowsPerPage = 5;
            int currentColumn = 0;
            int currentRow = 0;

            // --- RECURSOS ---
            using (Font fontName = new Font("Arial", 22, FontStyle.Bold))
            using (Font fontPrice = new Font("Arial Black", 28, FontStyle.Bold))
            using (Pen penBorder = new Pen(Color.Black, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            using (StringFormat nameFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisWord })
            using (StringFormat priceFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                while (_printIndex < _bindingList.Count)
                {
                    var item = _bindingList[_printIndex];

                    // Coordenadas base
                    float x = leftMargin + (currentColumn * labelWidth);
                    float y = topMargin + (currentRow * labelHeight);

                    // Borde
                    e.Graphics.DrawRectangle(penBorder, x, y, labelWidth, labelHeight);

                    // Área interna
                    RectangleF textArea = new RectangleF(x + padding, y + padding, labelWidth - (padding * 2), labelHeight - (padding * 2));

                    //
                    float nameHeight = textArea.Height * 0.35f;

                    RectangleF nameRect = new RectangleF(textArea.X, textArea.Y + nameTopPadding, textArea.Width, nameHeight);

                    e.Graphics.DrawString(item.PrintName, fontName, Brushes.Black, nameRect, nameFormat);

                    //PRECIO
                    RectangleF priceRect = new RectangleF(textArea.X, textArea.Y + nameHeight, textArea.Width, textArea.Height - nameHeight);

                    e.Graphics.DrawString($"$ {item.Price:N0}", fontPrice, Brushes.Black, priceRect, priceFormat);


                    _printIndex++;
                    currentColumn++;

                    if (currentColumn >= columnsPerPage)
                    {
                        currentColumn = 0;
                        currentRow++;
                    }

                    if (currentRow >= rowsPerPage)
                    {
                        if (_printIndex < _bindingList.Count)
                        {
                            e.HasMorePages = true;
                            return;
                        }
                    }
                }
            }

            e.HasMorePages = false;
        }

        private void btnToUpper_Click(object sender, EventArgs e)
        {
            if (dgvPrintProducts.CurrentCell == null) return;

            if (dgvPrintProducts.CurrentRow.DataBoundItem is ProductLabelDto item)
            {
                item.PrintName = item.PrintName.ToUpper();

                dgvPrintProducts.Refresh();
            }
        }

        private void btnAllToUpper_Click(object sender, EventArgs e)
        {
            // Si se está editando un nombre, se confirma antes para no pisar lo que se escribió.
            dgvPrintProducts.EndEdit();

            foreach (var item in _bindingList)
            {
                if (item.PrintName != null) item.PrintName = item.PrintName.ToUpper();
            }

            dgvPrintProducts.Refresh();
        }

        private void dgvPrintProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvPrintProducts.CurrentRow != null)
                {
                    if (dgvPrintProducts.CurrentRow.DataBoundItem is ProductLabelDto item)
                    {
                        _labelQueueService.RemoveFromQueue(item);

                        LoadGrid();
                    }
                }
            }
        }
    }
}

using Application.ProviderMovements.Dtos;
using Application.ProviderMovements.Interfaces;
using Application.Providers.Interfaces;
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

namespace WinForms.PopUps
{
    public partial class FormMovementEditor : Form
    {
        private readonly IProviderMovementService _movementService;
        private readonly IProviderService _providerService;
        private readonly IServiceProvider _serviceProvider;

        private int? _id = null;
        private string? _selectedFilePath = null;

        public FormMovementEditor(
            IProviderMovementService movementService,
            IProviderService providerService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _movementService = movementService;
            _providerService = providerService;
            _serviceProvider = serviceProvider;
        }

        private async void FormMovementEditor_Load(object sender, EventArgs e)
        {
            if (_id == null)
            {
                this.Text = "Registrar Compra";
                dtpDate.Value = DateTime.Now;
                lblArchive.Text = "Sin archivo seleccionado";
                await LoadProviders();
            }
        }

        private async Task LoadProviders(string? selectCuit = null)
        {
            try
            {
                var providers = await _providerService.GetAllForGridAsync();
                /*
                if (selectCuit != null)
                {
                    var list = providers.Select(p => new { Cuit = p.Cuit, Display = p.Name }).ToList();
                    list.Insert(0, new { Cuit = "ALL", Display = "TODOS LOS PROVEEDORES" });
                }*/

                cbxProviders.DataSource = providers.ToList();
                cbxProviders.DisplayMember = "Name";
                cbxProviders.ValueMember = "Cuit";

                if (selectCuit != null)
                {
                    cbxProviders.SelectedValue = selectCuit;
                }
                else
                {
                    cbxProviders.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message);
            }
        }

        public async void LoadForEdit(int id)
        {
            _id = id;
            this.Text = $"Editar Compra #{id}";
            btnAdd.Text = "Guardar";

            try
            {
                await LoadProviders();

                var dto = await _movementService.GetForEditAsync(id);

                if (dto != null)
                {
                    // Asignar valores
                    dtpDate.Value = dto.Date;
                    txtTotal.Text = dto.Total.ToString("0.00");

                    cbxProviders.SelectedValue = dto.ProviderCuit;
                    cbxProviders.Enabled = false;
                    btnNewProvider.Enabled = false;

                    // Archivo
                    if (!string.IsNullOrEmpty(dto.SourceFilePath)) // Usamos este campo para saber si tenía archivo o un flag
                    {
                        lblArchive.Text = "Archivo adjunto existente (Subir otro para reemplazar)";
                        lblArchive.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        lblArchive.Text = "Sin archivo adjunto";
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró el movimiento.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
                this.Close();
            }
        }

        private void btnSelectArchive_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Comprobantes (*.pdf;*.jpg;*.png)|*.pdf;*.jpg;*.png|Todos (*.*)|*.*";
                openFileDialog.Title = "Seleccionar Factura/Comprobante";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedFilePath = openFileDialog.FileName;
                    lblArchive.Text = Path.GetFileName(_selectedFilePath);
                    lblArchive.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            // Validaciones UI
            if (_id == null && cbxProviders.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTotal.Text))
            {
                MessageBox.Show("Ingrese el total.");
                return;
            }

            if (!decimal.TryParse(txtTotal.Text, out decimal total) || total <= 0)
            {
                MessageBox.Show("El total debe ser mayor a 0.");
                return;
            }

            DateTime date = DateTime.Now;
            if (!chkToday.Checked)
            {
                date = dtpDate.Value;
            }

            try
            {
                if (_id == null)
                {

                    // --- CREAR ---
                    var dto = new CreateProviderMovementDto
                    {
                        ProviderCuit = cbxProviders.SelectedValue.ToString(),
                        Date = date,
                        Total = total,
                        SourceFilePath = _selectedFilePath
                    };

                    await _movementService.CreateMovementAsync(dto);
                    MessageBox.Show("Compra registrada exitosamente.");
                }
                else
                {
                    // --- EDITAR ---
                    var dto = new UpdateProviderMovementDto
                    {
                        Id = _id.Value,
                        Date = date,
                        Total = total,
                        // Si _selectedFilePath es null, el servicio mantiene el viejo.
                        // Si tiene valor, el servicio reemplaza el archivo.
                        SourceFilePath = _selectedFilePath,
                        IsActive = true
                    };

                    await _movementService.UpdateMovementAsync(dto);
                    MessageBox.Show("Compra actualizada exitosamente.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnNewProvider_Click(object sender, EventArgs e)
        {
            // Creamos el formulario de proveedor al vuelo
            var editor = _serviceProvider.GetRequiredService<FormProviderEditor>();

            // Lo mostramos
            if (editor.ShowDialog() == DialogResult.OK)
            {
                await LoadProviders();
            }
        }

        private void chkToday_CheckedChanged(object sender, EventArgs e)
        {
            if (chkToday.Checked)
            {
                dtpDate.Enabled = false;
                dtpDate.Visible = false;
            }
            else
            {
                dtpDate.Enabled = true;
                dtpDate.Visible = true;
            }
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }
    }
}

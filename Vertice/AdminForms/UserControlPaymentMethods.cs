using Application.PaymentMethods.Interfaces;
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
using WinForms.PopUps;

namespace WinForms.AdminForms
{
    public partial class UserControlPaymentMethods : UserControl
    {
        private readonly IPaymentMethodService _service;
        private readonly IServiceProvider _serviceProvider;

        public UserControlPaymentMethods(IPaymentMethodService service, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _service = service;
            _serviceProvider = serviceProvider;
        }

        private async void LoadGrid()
        {
            try
            {
                var list = await _service.GetAllForGridAsync();
                dgvPaymentMethods.DataSource = list.ToList();
                ConfigGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista: " + ex.Message);
            }
        }

        private void ConfigGrid()
        {
            if (dgvPaymentMethods.Columns.Count == 0) return;

            dgvPaymentMethods.Columns["Id"].Visible = false;

            dgvPaymentMethods.Columns["Name"].HeaderText = "Nombre";

            dgvPaymentMethods.Columns["Recharge"].HeaderText = "Recargo %";
            dgvPaymentMethods.Columns["Recharge"].DefaultCellStyle.Format = "N2";

            dgvPaymentMethods.Columns["Discount"].HeaderText = "Descuento %";
            dgvPaymentMethods.Columns["Discount"].DefaultCellStyle.Format = "N2";

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPaymentMethods.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro para eliminar.");
                return;
            }

            int id = (int)dgvPaymentMethods.CurrentRow.Cells["Id"].Value;
            string nombre = dgvPaymentMethods.CurrentRow.Cells["Name"].Value.ToString();

            var confirm = MessageBox.Show($"¿Desea eliminar el método de pago '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    await _service.SoftDeleteAsync(id);
                    LoadGrid();
                    MessageBox.Show("Eliminado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editor = _serviceProvider.GetRequiredService<FormPaymentMethodEditor>();
            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadGrid();
            }
        }

        private void UserControlPaymentMethods_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPaymentMethods.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un registro para editar.");
                return;
            }

            //Obtener ID
            int id = (int)dgvPaymentMethods.CurrentRow.Cells["Id"].Value;

            var editor = _serviceProvider.GetRequiredService<FormPaymentMethodEditor>();
            editor.LoadForEdit(id);//Cargar para editar

            var result = editor.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadGrid();
            }
        }
    }
}

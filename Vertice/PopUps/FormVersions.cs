using Krypton.Toolkit;
using WinForms.Helpers;

namespace WinForms.PopUps
{
    /// <summary>
    /// Lista las versiones publicadas para instalar otra (normalmente, volver a una anterior).
    /// Solo elige: la instalación y los controles de compatibilidad los hace UserControlSettings.
    /// </summary>
    public class FormVersions : Form
    {
        private readonly DataGridView dgvVersions = new DataGridView();
        private readonly KryptonButton btnInstall = new KryptonButton();
        private readonly KryptonButton btnClose = new KryptonButton();

        public ReleaseInfo? SelectedRelease { get; private set; }

        public FormVersions()
        {
            BuildUi();
        }

        public void LoadReleases(IReadOnlyList<ReleaseInfo> releases)
        {
            var actual = UpdateService.CurrentVersion;

            dgvVersions.Rows.Clear();
            foreach (var r in releases)
            {
                string version = r.Version == actual ? $"{r.VersionText} (actual)" : r.VersionText;
                string notas = r.Notes.Replace("\r", " ").Replace("\n", " ");
                int index = dgvVersions.Rows.Add(version, r.PublishedAt.ToString("dd/MM/yyyy"), notas);
                dgvVersions.Rows[index].Tag = r;
            }
        }

        private void BuildUi()
        {
            Text = "Versiones publicadas";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(760, 440);
            BackColor = Color.FromArgb(238, 239, 249);
            Font = new Font("Microsoft Sans Serif", 11.25F);

            var lblTitle = new Label
            {
                Text = "VERSIONES PUBLICADAS",
                Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 25, 70),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Bounds = new Rectangle(20, 12, 720, 30)
            };

            dgvVersions.Bounds = new Rectangle(20, 50, 720, 310);
            dgvVersions.ReadOnly = true;
            dgvVersions.AllowUserToAddRows = false;
            dgvVersions.AllowUserToDeleteRows = false;
            dgvVersions.AllowUserToResizeRows = false;
            dgvVersions.MultiSelect = false;
            dgvVersions.RowHeadersVisible = false;
            dgvVersions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVersions.BackgroundColor = Color.White;
            dgvVersions.BorderStyle = BorderStyle.None;
            dgvVersions.EnableHeadersVisualStyles = false;
            dgvVersions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 70);
            dgvVersions.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
            dgvVersions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            dgvVersions.DefaultCellStyle.SelectionBackColor = Color.ForestGreen;
            dgvVersions.RowTemplate.Height = 32;
            dgvVersions.Columns.Add("Version", "Versión");
            dgvVersions.Columns.Add("Fecha", "Fecha");
            dgvVersions.Columns.Add("Notas", "Novedades");
            dgvVersions.Columns[0].Width = 130;
            dgvVersions.Columns[1].Width = 110;
            dgvVersions.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvVersions.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) SelectAndClose(); };

            ConfigButton(btnInstall, "INSTALAR ESTA VERSIÓN", new Rectangle(190, 375, 230, 42));
            ConfigButton(btnClose, "CERRAR", new Rectangle(440, 375, 130, 42));

            btnInstall.Click += (s, e) => SelectAndClose();
            btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            CancelButton = btnClose;

            Controls.AddRange(new Control[] { lblTitle, dgvVersions, btnInstall, btnClose });
        }

        private static void ConfigButton(KryptonButton btn, string text, Rectangle bounds)
        {
            btn.Bounds = bounds;
            btn.Values.Text = text;
            btn.Values.DropDownArrowColor = Color.Empty;
            btn.StateCommon.Back.Color1 = Color.FromArgb(160, 205, 254);
            btn.StateCommon.Content.ShortText.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
        }

        private void SelectAndClose()
        {
            if (dgvVersions.CurrentRow?.Tag is not ReleaseInfo release) return;

            if (release.Version == UpdateService.CurrentVersion)
            {
                MessageBox.Show("Esa es la versión que ya está instalada.", "Versiones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectedRelease = release;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

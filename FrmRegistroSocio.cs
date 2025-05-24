using System;
using System.Windows.Forms;
using System.Text;
using System.Drawing;

namespace ClubDeportivo
{
    public class FrmRegistroSocio : Form
    {
        private SistemaClub sistema;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtDni;
        private TextBox txtNumeroSocio;
        private TextBox txtUsuario;
        private TextBox txtClave;
        private Button btnRegistrar;

        public FrmRegistroSocio(SistemaClub sistemaClub)
        {
            this.sistema = sistemaClub;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Registro de Socio";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;

            int y = 20, xLabel = 20, xText = 120, spacing = 30;

            // Nombre
            Label lblNombre = new Label() { Text = "Nombre:", Location = new Point(xLabel, y) };
            this.Controls.Add(lblNombre);
            txtNombre = new TextBox() { Location = new Point(xText, y), Width = 150 };
            this.Controls.Add(txtNombre);
            y += spacing;

            // Apellido
            Label lblApellido = new Label() { Text = "Apellido:", Location = new Point(xLabel, y) };
            this.Controls.Add(lblApellido);
            txtApellido = new TextBox() { Location = new Point(xText, y), Width = 150 };
            this.Controls.Add(txtApellido);
            y += spacing;

            // DNI
            Label lblDni = new Label() { Text = "DNI:", Location = new Point(xLabel, y) };
            this.Controls.Add(lblDni);
            txtDni = new TextBox() { Location = new Point(xText, y), Width = 150 };
            this.Controls.Add(txtDni);
            y += spacing;

            // Número de Socio
            Label lblNumeroSocio = new Label() { Text = "Número Socio:", Location = new Point(xLabel, y) };
            this.Controls.Add(lblNumeroSocio);
            txtNumeroSocio = new TextBox() { Location = new Point(xText, y), Width = 150 };
            this.Controls.Add(txtNumeroSocio);
            y += spacing;

            // Usuario
            Label lblUsuario = new Label() { Text = "Usuario:", Location = new Point(xLabel, y) };
            this.Controls.Add(lblUsuario);
            txtUsuario = new TextBox() { Location = new Point(xText, y), Width = 150 };
            this.Controls.Add(txtUsuario);
            y += spacing;

            // Clave
            Label lblClave = new Label() { Text = "Clave:", Location = new Point(xLabel, y) };
            this.Controls.Add(lblClave);
            txtClave = new TextBox() { Location = new Point(xText, y), Width = 150, UseSystemPasswordChar = true };
            this.Controls.Add(txtClave);
            y += spacing;

            // Botón Registrar
            btnRegistrar = new Button()
            {
                Text = "Registrar",
                Location = new Point(xText, y),
                Width = 100
            };
            btnRegistrar.Click += new EventHandler(btnRegistrar_Click);
            this.Controls.Add(btnRegistrar);
        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Socio nuevoSocio = new Socio(
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtDni.Text.Trim(),
                    Convert.ToInt32(txtNumeroSocio.Text),
                    txtUsuario.Text.Trim(),
                    txtClave.Text
                );

                sistema.RegistrarSocio(nuevoSocio);

                // Mensaje de éxito corregido
                MessageBox.Show("Socio registrado exitosamente", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cierra y vuelve a abrir el formulario de listado para refrescar
                // Notificar al formulario listado para que actualice
                if (this.Owner is FrmListarSocios)
                {
                    FrmListarSocios listado = (FrmListarSocios)this.Owner;
                    listado.Close();
                    FrmListarSocios newListado = new FrmListarSocios(sistema);
                    newListado.Show(this.Owner);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                // Mensaje de error corregido
                MessageBox.Show("Error al registrar socio: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


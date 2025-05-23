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
                    txtNombre.Text,
                    txtApellido.Text,
                    txtDni.Text,
                    Convert.ToInt32(txtNumeroSocio.Text),
                    txtUsuario.Text,
                    txtClave.Text
                );

                sistema.RegistrarSocio(nuevoSocio);

                MessageBox.Show("Socio registrado exitosamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

/*using System;
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
    private Button btnRegistrar;

    public FrmRegistroSocio(SistemaClub sistemaClub)
    {
        this.sistema = sistemaClub;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // Configuración básica del formulario
        this.Text = "Registro de Socio";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
         

        // Label y TextBox para Nombre
        Label lblNombre = new Label();
        lblNombre.Text = "Nombre:";
        lblNombre.Location = new System.Drawing.Point(20, 20);
        this.Controls.Add(lblNombre);

        txtNombre = new TextBox();
        txtNombre.Location = new System.Drawing.Point(120, 20);
        txtNombre.Width = 150;
        this.Controls.Add(txtNombre);

        // Label y TextBox para Apellido
        Label lblApellido = new Label();
        lblApellido.Text = "Apellido:";
        lblApellido.Location = new System.Drawing.Point(20, 50);
        this.Controls.Add(lblApellido);

        txtApellido = new TextBox();
        txtApellido.Location = new System.Drawing.Point(120, 50);
        txtApellido.Width = 150;
        this.Controls.Add(txtApellido);

        // Label y TextBox para DNI
        Label lblDni = new Label();
        lblDni.Text = "DNI:";
        lblDni.Location = new System.Drawing.Point(20, 80);
        this.Controls.Add(lblDni);

        txtDni = new TextBox();
        txtDni.Location = new System.Drawing.Point(120, 80);
        txtDni.Width = 150;
        this.Controls.Add(txtDni);

        // Label y TextBox para Número de Socio
        Label lblNumeroSocio = new Label();
        lblNumeroSocio.Text = "Número Socio:";
        lblNumeroSocio.Location = new System.Drawing.Point(20, 110);
        this.Controls.Add(lblNumeroSocio);

        txtNumeroSocio = new TextBox();
        txtNumeroSocio.Location = new System.Drawing.Point(120, 110);
        txtNumeroSocio.Width = 150;
        this.Controls.Add(txtNumeroSocio);

        // Botón Registrar
        btnRegistrar = new Button();
        btnRegistrar.Text = "Registrar";
        btnRegistrar.Location = new System.Drawing.Point(120, 150);
        btnRegistrar.Click += new EventHandler(btnRegistrar_Click);
        this.Controls.Add(btnRegistrar);
    }

  
private void btnRegistrar_Click(object sender, EventArgs e)
{
    try
    {
        Socio nuevoSocio = new Socio(
            txtNombre.Text,
            txtApellido.Text,
            txtDni.Text,
            Convert.ToInt32(txtNumeroSocio.Text)
        );

        sistema.RegistrarSocio(nuevoSocio); // usar el que vino del constructor

        MessageBox.Show("Socio registrado exitosamente");
        this.Close();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message);
    }
}

}
}*/
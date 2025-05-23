using System;
using System.Windows.Forms;
using System.Text;
using System.Drawing;

namespace ClubDeportivo
{
public class FrmRegistroNoSocio : Form
{
    private SistemaClub sistema;
    private TextBox txtNombre;
    private TextBox txtApellido;
    private TextBox txtDni;
    private TextBox txtNumeroSocio;
    //private Button btnRegistrar;
        private Button btnRegistrarNoSocio;//no socio

        public FrmRegistroNoSocio(SistemaClub sistemaClub)
    {
        this.sistema = sistemaClub;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // Configuración básica del formulario
        this.Text = "Registro de No Socios";
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

        // Label y TextBox para Número de No Socio
        Label lblNumeroNoSocio = new Label();
        lblNumeroNoSocio.Text = "Número No Socio:";
        lblNumeroNoSocio.Location = new System.Drawing.Point(20, 110);
        this.Controls.Add(lblNumeroNoSocio);

        txtNumeroSocio = new TextBox();
        txtNumeroSocio.Location = new System.Drawing.Point(120, 110);
        txtNumeroSocio.Width = 150;
        this.Controls.Add(txtNumeroSocio);

        // Botón Registrar
        btnRegistrarNoSocio = new Button();
        btnRegistrarNoSocio.Text = "Registrar No Socio";
        btnRegistrarNoSocio.Location = new System.Drawing.Point(120, 150);
        btnRegistrarNoSocio.Click += new EventHandler(btnRegistrarNoSocio_Click);
        this.Controls.Add(btnRegistrarNoSocio);
    }

  
private void btnRegistrarNoSocio_Click(object sender, EventArgs e)
{
    try
    {
        NoSocio nuevoNoSocio = new NoSocio(
            txtNombre.Text,
            txtApellido.Text,
            txtDni.Text,
            Convert.ToInt32(txtNumeroSocio.Text)
        );

        sistema.RegistrarNoSocio(nuevoNoSocio); // usar el que vino del constructor

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
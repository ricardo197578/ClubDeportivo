using System;
using System.Windows.Forms;

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
        this.Width = 300;
        this.Height = 250;

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
}
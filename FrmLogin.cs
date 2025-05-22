using System;
using System.Windows.Forms;
using System.Drawing;

namespace ClubDeportivo
{
public class FrmLogin : Form
{
    private SistemaClub sistema;
    private TextBox txtUsuario;
    private TextBox txtClave;
    private Button btnIngresar;
    private RadioButton rbAdmin;
    private RadioButton rbSocio;

    public FrmLogin(SistemaClub sistemaClub)
    {
        this.sistema = sistemaClub;
            Console.WriteLine("FrmLogin ha sido instanciado correctamente.");
            InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "Login - Club Deportivo";
        this.Width = 300;
        this.Height = 250;

        // RadioButtons para selección de tipo de usuario
        rbAdmin = new RadioButton();
        rbAdmin.Text = "Administrador";
        rbAdmin.Location = new System.Drawing.Point(20, 20);
        rbAdmin.Checked = true;
        this.Controls.Add(rbAdmin);

        rbSocio = new RadioButton();
        rbSocio.Text = "Socio";
        rbSocio.Location = new System.Drawing.Point(20, 50);
        this.Controls.Add(rbSocio);

        // Campos de texto
        Label lblUsuario = new Label();
        lblUsuario.Text = "Usuario/N° Socio:";
        lblUsuario.Location = new System.Drawing.Point(20, 80);
        this.Controls.Add(lblUsuario);

        txtUsuario = new TextBox();
        txtUsuario.Location = new System.Drawing.Point(120, 80);
        txtUsuario.Width = 150;
        this.Controls.Add(txtUsuario);

        Label lblClave = new Label();
        lblClave.Text = "Clave:";
        lblClave.Location = new System.Drawing.Point(20, 110);
        this.Controls.Add(lblClave);

        txtClave = new TextBox();
        txtClave.Location = new System.Drawing.Point(120, 110);
        txtClave.Width = 150;
        txtClave.PasswordChar = '*';
        txtClave.Enabled = false; // Solo habilitado para admin
        this.Controls.Add(txtClave);

        // Botón Ingresar
        btnIngresar = new Button();
        btnIngresar.Text = "Ingresar";
        btnIngresar.Location = new System.Drawing.Point(120, 150);
        btnIngresar.Click += new EventHandler(btnIngresar_Click);
        this.Controls.Add(btnIngresar);

        // Eventos para RadioButtons
        rbAdmin.CheckedChanged += (s, e) => txtClave.Enabled = rbAdmin.Checked;
        rbSocio.CheckedChanged += (s, e) => txtClave.Enabled = !rbSocio.Checked;

    }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (rbAdmin.Checked)
            {
                if (sistema.ValidarAdmin(txtUsuario.Text, txtClave.Text))
                {
                    MessageBox.Show("Acceso concedido como Administrador");
                    this.Hide();
                    new FrmAdmin(sistema).Show();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas");
                }
            }
            else
            {
                int numeroSocio;
                if (int.TryParse(txtUsuario.Text, out numeroSocio))
                {
                    Socio socio = sistema.ValidarSocio(numeroSocio);
                    if (socio != null)
                    {
                        // Primero muestra los datos del socio
                        FrmDatosSocio frmDatos = new FrmDatosSocio(socio);
                        DialogResult result = frmDatos.ShowDialog();

                        // Solo abre el pago si el usuario lo solicita (ej: con un botón)
                        if (result == DialogResult.OK) // Asumiendo que lo cierras con this.DialogResult=DialogResult.OK
                        {
                            this.Hide();
                            new FrmPagarCuota(socio).Show();
                        }
                    }
                }
            }
        }
       

        /* private void btnIngresar_Click(object sender, EventArgs e)
         {
             if (rbAdmin.Checked)
             {
                 if (sistema.ValidarAdmin(txtUsuario.Text, txtClave.Text))
                 {
                     MessageBox.Show("Acceso concedido como Administrador");
                     this.Hide();
                     new FrmAdmin(sistema).Show();
                 }
                 else
                 {
                     MessageBox.Show("Credenciales incorrectas");
                 }
             }
             else
             {
                 int numeroSocio;
                 if (int.TryParse(txtUsuario.Text, out numeroSocio))
                 {
                     Socio socio = sistema.ValidarSocio(numeroSocio);
                     if (socio != null)
                     {
                         MessageBox.Show(string.Format("Bienvenido, Socio {0}", socio.Nombre));
                         this.Hide();
                         new FrmDatosSocio(socio).Show();
                     }
                     else
                     {
                         MessageBox.Show("Número de socio no válido");
                     }
                 }
                 else
                 {
                     MessageBox.Show("Ingrese un número de socio válido");
                 }
             }

             }*/
    }
}
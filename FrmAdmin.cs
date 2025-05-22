using System;
using System.Windows.Forms;
using System.Drawing;

namespace ClubDeportivo
{

public class FrmAdmin : Form
{
    private SistemaClub sistema;
    private Button btnListarSocios;
        private Button btnListarNoSocios;
        private Button btnRegistrarSocio;
    //private Button btnRegistrarNoSocio;

    public FrmAdmin(SistemaClub sistemaClub)
    {
        this.sistema = sistemaClub;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "Panel de Administrador";
        this.Width = 300;
        this.Height = 300;

        btnListarSocios = new Button();
        btnListarSocios.Text = "Listar Socios";
        btnListarSocios.Location = new Point(100, 20);
        btnListarSocios.Click += (s, e) => new FrmListarSocios(sistema).ShowDialog();
        this.Controls.Add(btnListarSocios);

            btnListarNoSocios = new Button();
            btnListarNoSocios.Text = "Listar No Socios";
            btnListarNoSocios.Location = new Point(100, 60);
            btnListarNoSocios.Click += (s, e) => new FrmListarNoSocios(sistema).ShowDialog();
            this.Controls.Add(btnListarNoSocios);

            btnRegistrarSocio = new Button();
        btnRegistrarSocio.Text = "Registrar Socio";
        btnRegistrarSocio.Location = new Point(100, 100);
        btnRegistrarSocio.Click += (s, e) => new FrmRegistroSocio(sistema).ShowDialog();
        this.Controls.Add(btnRegistrarSocio);


          
           
        }
}
}
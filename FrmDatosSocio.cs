using System;
using System.Windows.Forms;
using System.Drawing;

namespace ClubDeportivo
{

public class FrmDatosSocio : Form
{
    private Socio socio;

    public FrmDatosSocio(Socio socioActual)
    {
        this.socio = socioActual;
        InitializeComponent();
        MostrarDatos();
    }

    private void InitializeComponent()
    {
        this.Text = "Mis Datos - Club Deportivo";
        this.Width = 350;
        this.Height = 350;

        Button btnVolver = new Button();
        btnVolver.Text = "Volver";
        btnVolver.Location = new Point(120, 220);
        btnVolver.Click += (s, e) => this.Close();
        this.Controls.Add(btnVolver);

            Button btnPagarCuota = new Button();
            btnPagarCuota.Text = "Pagar Cuota";
            btnPagarCuota.Location = new Point(120, 250);
            btnPagarCuota.Click += (s, e) => new FrmPagarCuota(socio).ShowDialog();
            this.Controls.Add(btnPagarCuota);



        }

        private void MostrarDatos()
    {
        Label lblTitulo = new Label();
        lblTitulo.Text = "Mis Datos";
        lblTitulo.Font = new Font("Arial", 12, FontStyle.Bold);
        lblTitulo.Location = new Point(20, 20);
        lblTitulo.AutoSize = true;
        this.Controls.Add(lblTitulo);

        // Usando string.Format
        this.Controls.Add(new Label { Text = string.Format("Nombre: {0}", socio.Nombre), Location = new Point(20, 60) });
        this.Controls.Add(new Label { Text = string.Format("Apellido: {0}", socio.Apellido), Location = new Point(20, 90) });
        this.Controls.Add(new Label { Text = string.Format("DNI: {0}", socio.Dni), Location = new Point(20, 120) });
        this.Controls.Add(new Label { Text = string.Format("N° Socio: {0}", socio.NumeroSocio), Location = new Point(20, 150) });
        this.Controls.Add(new Label { Text = string.Format("Fecha Registro: {0}", socio.FechaRegistro.ToShortDateString()), Location = new Point(20, 180) });
    }
}
}
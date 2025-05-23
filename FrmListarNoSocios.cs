using System;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Data.SQLite;

namespace ClubDeportivo
{
public class FrmListarNoSocios : Form
{
    private SistemaClub sistema;
    private TextBox txtLista;

    public FrmListarNoSocios(SistemaClub sistemaClub)
    {
        this.sistema = sistemaClub;
        InitializeComponent();
        MostrarNoSocios();
    }

    private void InitializeComponent()
    {
        this.Text = "Listado de No Socios";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            txtLista = new TextBox();
        txtLista.Multiline = true;
        txtLista.ScrollBars = ScrollBars.Vertical;
        txtLista.Location = new Point(20, 20);
        txtLista.Width = 350;
        txtLista.Height = 200;
        txtLista.ReadOnly = true;
        this.Controls.Add(txtLista);
    }
            
	private void MostrarNoSocios()
{
    StringBuilder sb = new StringBuilder();
    using (SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.GetConnection()))
    {
        cmd.CommandText = @"
            SELECT p.Nombre, p.Apellido, p.Dni, s.NumeroNoSocio, s.FechaRegistro 
            FROM NoSocios s 
            JOIN Personas p ON s.PersonaId = p.Id";

        using (SQLiteDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                sb.AppendLine(string.Format(
                    "Nombre: {0} {1}, DNI: {2}, N° NoSocio: {3}, Fecha: {4}",
                    reader["Nombre"],
                    reader["Apellido"],
                    reader["Dni"],
                    reader["NumeroNoSocio"],
                    reader["FechaRegistro"]
                ));
            }
        }
    }
    txtLista.Text = sb.ToString();
}
}
}

using System;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Data.SQLite;

namespace ClubDeportivo
{
public class FrmListarSocios : Form
{
    private SistemaClub sistema;
    private TextBox txtLista;

    public FrmListarSocios(SistemaClub sistemaClub)
    {
        this.sistema = sistemaClub;
        InitializeComponent();
        MostrarSocios();
    }

    private void InitializeComponent()
    {
        this.Text = "Listado de Socios";
        this.Width = 400;
        this.Height = 300;

        txtLista = new TextBox();
        txtLista.Multiline = true;
        txtLista.ScrollBars = ScrollBars.Vertical;
        txtLista.Location = new Point(20, 20);
        txtLista.Width = 350;
        txtLista.Height = 200;
        txtLista.ReadOnly = true;
        this.Controls.Add(txtLista);
    }
            
	private void MostrarSocios()
{
    StringBuilder sb = new StringBuilder();
    using (SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.GetConnection()))
    {
        cmd.CommandText = @"
            SELECT p.Nombre, p.Apellido, p.Dni, s.NumeroSocio, s.FechaRegistro 
            FROM Socios s 
            JOIN Personas p ON s.PersonaId = p.Id";

        using (SQLiteDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                sb.AppendLine(string.Format(
                    "Nombre: {0} {1}, DNI: {2}, N° Socio: {3}, Fecha: {4}",
                    reader["Nombre"],
                    reader["Apellido"],
                    reader["Dni"],
                    reader["NumeroSocio"],
                    reader["FechaRegistro"]
                ));
            }
        }
    }
    txtLista.Text = sb.ToString();
}
}
}
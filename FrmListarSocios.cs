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
        private Button btnCerrar;

        public FrmListarSocios(SistemaClub sistemaClub)
        {
            this.sistema = sistemaClub;
            InitializeComponent();
            MostrarSocios();
        }

        private void InitializeComponent()
        {
            this.Text = "Listado de Socios";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            txtLista = new TextBox();
            txtLista.Multiline = true;
            txtLista.ScrollBars = ScrollBars.Vertical;
            txtLista.Location = new Point(20, 20);
            txtLista.Size = new Size(440, 280);
            txtLista.ReadOnly = true;
            txtLista.Font = new Font("Segoe UI", 10);
            txtLista.BackColor = Color.White;
            this.Controls.Add(txtLista);

            btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Size = new Size(100, 30);
            btnCerrar.Location = new Point((this.ClientSize.Width - btnCerrar.Width) / 2, 320);
            btnCerrar.Anchor = AnchorStyles.Bottom;
            btnCerrar.Click += (sender, e) => this.Close();
            this.Controls.Add(btnCerrar);
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
                            Convert.ToDateTime(reader["FechaRegistro"]).ToShortDateString()
                        ));
                        sb.AppendLine("------------------------------------------------------------");
                    }
                }
            }
            txtLista.Text = sb.ToString();
        }
    }
}

/*using System;
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
}*/
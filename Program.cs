using System;
using System.Windows.Forms;
using System.Data.SQLite;
using ClubDeportivo;

namespace ClubDeportivo
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            // Crear admin por defecto si no existe
            using (SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.GetConnection()))
            {
                cmd.CommandText = "SELECT COUNT(*) FROM Administradores";
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 0)
                {
                    cmd.CommandText = @"
                        INSERT INTO Personas (Nombre, Apellido, Dni, Tipo) 
                        VALUES ('Admin', 'Sistema', '00000000', 'Administrador')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT last_insert_rowid()";
                    int id = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.CommandText = @"
                        INSERT INTO Administradores (PersonaId, Usuario, Clave) 
                        VALUES (@id, 'admin', '1234')";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            // Iniciar el formulario de login
            Application.Run(new FrmLogin(new SistemaClub()));
        }
    }
}

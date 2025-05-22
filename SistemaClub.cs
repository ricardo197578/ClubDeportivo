using System; 
using System.Data.SQLite;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ClubDeportivo
{
    public class SistemaClub
    {
        public bool ValidarAdmin(string usuario, string clave)
        {
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.GetConnection()))
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Administradores WHERE Usuario = @usuario AND Clave = @clave";
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    // Debug: Muestra qué credenciales se están verificando
                    Console.WriteLine("Intento de login - Usuario: " + usuario + ", Clave: " + clave);

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar admin: " + ex.Message);
                return false;
            }
        }

        public Socio ValidarSocio(int numeroSocio)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.GetConnection()))
            {
                cmd.CommandText = @"
            SELECT p.Nombre, p.Apellido, p.Dni, s.NumeroSocio, s.FechaRegistro 
            FROM Socios s 
            JOIN Personas p ON s.PersonaId = p.Id 
            WHERE s.NumeroSocio = @numero";
                cmd.Parameters.AddWithValue("@numero", numeroSocio);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Socio socio = new Socio(
                            reader["Nombre"].ToString(),
                            reader["Apellido"].ToString(),
                            reader["Dni"].ToString(),
                            Convert.ToInt32(reader["NumeroSocio"])
                        )
                        {
                            FechaRegistro = DateTime.Parse(reader["FechaRegistro"].ToString())
                        };

                        // Cargar las cuotas del socio desde la BD
                        socio.Cuotas = ObtenerCuotas(numeroSocio); 
                        return socio;
                    }
                }
            }
            return null;
        }

        public void RegistrarSocio(Socio socio)
        {
            using (SQLiteTransaction transaction = DatabaseManager.GetConnection().BeginTransaction())
            {
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.GetConnection()))
                    {
                        // Insertar en Personas
                        cmd.CommandText = @"
                            INSERT INTO Personas (Nombre, Apellido, Dni, Tipo) 
                            VALUES (@nombre, @apellido, @dni, 'Socio')";
                        cmd.Parameters.AddWithValue("@nombre", socio.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", socio.Apellido);
                        cmd.Parameters.AddWithValue("@dni", socio.Dni);
                        cmd.ExecuteNonQuery();

                        // Obtener ID
                        cmd.CommandText = "SELECT last_insert_rowid()";
                        int id = Convert.ToInt32(cmd.ExecuteScalar());

                        // Insertar en Socios
                        cmd.CommandText = @"
                            INSERT INTO Socios (PersonaId, NumeroSocio, FechaRegistro) 
                            VALUES (@id, @numero, @fecha)";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@numero", socio.NumeroSocio);
                        cmd.Parameters.AddWithValue("@fecha", socio.FechaRegistro.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    GenerarCuotaParaSocio(socio.NumeroSocio, socio.FechaRegistro);
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        //VALIDAR INGRESO NOSOCIO
        public NoSocio ValidarNoSocio(int numeroNoSocio)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.GetConnection()))
            {
                cmd.CommandText = @"
                    SELECT p.Nombre, p.Apellido, p.Dni, s.NumeroNoSocio, s.FechaRegistro 
                    FROM NoSocios s 
                    JOIN Personas p ON s.PersonaId = p.Id 
                    WHERE s.NumeroNoSocio = @numero";
                cmd.Parameters.AddWithValue("@numero", numeroNoSocio);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new NoSocio(
                            reader["Nombre"].ToString(),
                            reader["Apellido"].ToString(),
                            reader["Dni"].ToString(),
                            Convert.ToInt32(reader["NumeroNoSocio"])
                        )
                        {
                            FechaRegistro = DateTime.Parse(reader["FechaRegistro"].ToString())
                        };
                    }
                }
            }
            return null;
        }
        public void RegistrarNoSocio(NoSocio noSocio)
        {
            using (SQLiteTransaction transaction = DatabaseManager.GetConnection().BeginTransaction())
            {
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(DatabaseManager.GetConnection()))
                    {
                        // Insertar en Personas
                        cmd.CommandText = @"
                            INSERT INTO Personas (Nombre, Apellido, Dni, Tipo) 
                            VALUES (@nombre, @apellido, @dni, 'Socio')";
                        cmd.Parameters.AddWithValue("@nombre", noSocio.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", noSocio.Apellido);
                        cmd.Parameters.AddWithValue("@dni", noSocio.Dni);
                        cmd.ExecuteNonQuery();

                        // Obtener ID
                        cmd.CommandText = "SELECT last_insert_rowid()";
                        int id = Convert.ToInt32(cmd.ExecuteScalar());

                        // Insertar en NoSocios
                        cmd.CommandText = @"
                            INSERT INTO NoSocios (PersonaId, NumeroNoSocio, FechaRegistro) 
                            VALUES (@id, @numero, @fecha)";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@numero", noSocio.NumeroNoSocio);
                        cmd.Parameters.AddWithValue("@fecha", noSocio.FechaRegistro.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        //agregado
        public void GenerarCuotaParaSocio(int numeroSocio, DateTime fechaRegistro)
        {
            DateTime hoy = DateTime.Now;
            int mesesAdeudados = ((hoy.Year - fechaRegistro.Year) * 12) + hoy.Month - fechaRegistro.Month;

            // Si es un nuevo registro, aseguramos que tenga al menos una cuota inicial
            if (mesesAdeudados == 0) mesesAdeudados = 1;

            using (var conn = DatabaseManager.GetConnection())
            {
                for (int i = 0; i < mesesAdeudados; i++)
                {
                    DateTime fechaCuota = fechaRegistro.AddMonths(i);

                    using (var cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = @"
                    INSERT INTO Cuotas (NumeroSocio, FechaPago, Monto, Pagado)
                    VALUES (@numeroSocio, @fechaCuota, @monto, 0)";
                        cmd.Parameters.AddWithValue("@numeroSocio", numeroSocio);
                        cmd.Parameters.AddWithValue("@fechaCuota", fechaCuota.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@monto", 500);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


        //agregado
        public void GenerarCuotasPendientes()
        {
            using (var conn = DatabaseManager.GetConnection())
            using (var cmd = new SQLiteCommand("SELECT NumeroSocio, FechaRegistro FROM Socios", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int numeroSocio = Convert.ToInt32(reader["NumeroSocio"]);
                    DateTime fechaRegistro = DateTime.Parse(reader["FechaRegistro"].ToString());

                    // Generar cuotas automáticamente para el socio
                    GenerarCuotaParaSocio(numeroSocio, fechaRegistro);
                }
            }
        }



        //PARA OBTENER CUOTAS
        public List<Cuota> ObtenerCuotas(int numeroSocio)
        {
            List<Cuota> cuotas = new List<Cuota>();

            using (var conn = DatabaseManager.GetConnection())
            using (var cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = @"
            SELECT Id, NumeroSocio, FechaPago, Monto, Pagado 
            FROM Cuotas WHERE NumeroSocio = @numero";
                cmd.Parameters.AddWithValue("@numero", numeroSocio);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cuotas.Add(new Cuota(
                            Convert.ToInt32(reader["Id"]),
                            Convert.ToInt32(reader["NumeroSocio"]),
                            DateTime.Parse(reader["FechaPago"].ToString()),
                            Convert.ToDecimal(reader["Monto"]),
                            Convert.ToInt32(reader["Pagado"]) == 1
                        ));
                    }
                }
            }

            return cuotas;
        }


    }
}

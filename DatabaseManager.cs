using System;
using System.Data.SQLite;

namespace ClubDeportivo
{
    public static class DatabaseManager
    {
        private static string databasePath = "ClubDeportivo.db";
        private static SQLiteConnection connection;

        public static SQLiteConnection GetConnection()
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {
                connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", databasePath));
                connection.Open();
                InitializeDatabase();
            }
            return connection;
        }

        private static void InitializeDatabase()
        {
            using (SQLiteCommand cmd = new SQLiteCommand(connection))
            {
                // Tabla Personas
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Personas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Apellido TEXT NOT NULL,
                    Dni TEXT UNIQUE NOT NULL,
                    Tipo TEXT NOT NULL
                )";
                cmd.ExecuteNonQuery();

                // Tabla Socios
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Socios (
                    PersonaId INTEGER PRIMARY KEY,
                    NumeroSocio INTEGER UNIQUE NOT NULL,
                    FechaRegistro TEXT NOT NULL,
                    Usuario TEXT UNIQUE NOT NULL,
                    Clave TEXT NOT NULL,
                    FOREIGN KEY (PersonaId) REFERENCES Personas(Id)
                )";
                cmd.ExecuteNonQuery();

                // Tabla Socios
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS NoSocios (
                    PersonaId INTEGER PRIMARY KEY,
                    NumeroNoSocio INTEGER UNIQUE NOT NULL,
                    FechaRegistro TEXT NOT NULL,
                    FOREIGN KEY (PersonaId) REFERENCES Personas(Id)
                )";
                cmd.ExecuteNonQuery();

                // Tabla Administradores
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Administradores (
                    PersonaId INTEGER PRIMARY KEY,
                    Usuario TEXT UNIQUE NOT NULL,
                    Clave TEXT NOT NULL,
                    FOREIGN KEY (PersonaId) REFERENCES Personas(Id)
                )";
                cmd.ExecuteNonQuery();

                //TALA CUOTAS
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Cuotas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    NumeroSocio INTEGER NOT NULL,
                    FechaPago TEXT NOT NULL,
                    Monto REAL NOT NULL,
                    Pagado INTEGER NOT NULL,
                    FOREIGN KEY (NumeroSocio) REFERENCES Socios(NumeroSocio)
                 )";
                cmd.ExecuteNonQuery();

            }
        }
       
    }
}

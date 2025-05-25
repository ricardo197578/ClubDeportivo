using System;
using System.Data.SQLite;

namespace ClubDeportivo
{
    public static class DatabaseManager
    {
        private static string databasePath = "ClubDeportivo.db";
        private static SQLiteConnection _connection;
        private static readonly object _lock = new object();

        public static SQLiteConnection GetConnection()
        {
            lock (_lock)
            {
                if (_connection == null)
                {
                    _connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", databasePath));
                    _connection.Open();
                    InitializeDatabase();
                }
                else if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }
        public static SQLiteConnection GetNewConnection()
        {
            var connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", databasePath));
            connection.Open();
            return connection;
        }
       
        private static void InitializeDatabase()
        {
            using (SQLiteCommand cmd = new SQLiteCommand(_connection))
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

        public static void CloseConnection()
        {
            lock (_lock)
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
    }
}

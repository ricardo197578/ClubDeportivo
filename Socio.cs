using System;
using System.Collections.Generic;
using System.Data.SQLite;


namespace ClubDeportivo
{

    public class Socio : Persona
    {
        public int NumeroSocio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<Cuota> Cuotas { get; set; }//agregado
        public string Usuario { get; set; }
        public string Clave { get; set; }
        /*
        
        FALTA IMPLEMENTAR
        public AptoFisico AptFisico { get; set; }
        public bool EstadoActivo { get; set; }
        public List<Cuota> HistorialCuotas { get; set; }
        public List<Actividad> ActividadesInscritas { get; set; }
        public Carnet Carnet { get; set; }
         */

        public Socio(string nombre, string apellido, string dni, int numeroSocio, string usuario,string clave)
            : base(nombre, apellido, dni)
        {
            NumeroSocio = numeroSocio;
            FechaRegistro = DateTime.Now;
            Cuotas = new List<Cuota>();//inicializacion agregdo
            Usuario = usuario;
            Clave = clave;
        }
    

       
        //calcular cuota
        public int CalcularCuotasAdeudadas()
        {
            DateTime hoy = DateTime.Now;
            int mesesAdeudados = ((hoy.Year - FechaRegistro.Year) * 12) + hoy.Month - FechaRegistro.Month;
            return mesesAdeudados;
        }
        public void PagarCuota(int cuotaId)
        {
            using (var connection = DatabaseManager.GetNewConnection())
            {
                using (var cmd = new SQLiteCommand(connection))
                {
                    cmd.CommandText = "UPDATE Cuotas SET Pagado = 1 WHERE Id = @id AND NumeroSocio = @numeroSocio";
                    cmd.Parameters.AddWithValue("@id", cuotaId);
                    cmd.Parameters.AddWithValue("@numeroSocio", this.NumeroSocio);
                    cmd.ExecuteNonQuery();
                }
            }

            // Actualizar también en memoria
            var cuota = this.Cuotas.Find(c => c.Id == cuotaId);
            if (cuota != null)
            {
                cuota.Pagado = true;
            }
        }

    }
}
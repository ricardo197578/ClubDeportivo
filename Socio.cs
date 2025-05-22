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


        public Socio(string nombre, string apellido, string dni, int numeroSocio)
            : base(nombre, apellido, dni)
        {
            NumeroSocio = numeroSocio;
            FechaRegistro = DateTime.Now;
            Cuotas = new List<Cuota>();//inicializacion agregdo
        }
    

        //metodo pagar cuota
        public void PagarCuota(int cuotaId)
        {
            using (var conn = DatabaseManager.GetConnection())
            using (var cmd = new SQLiteCommand(conn))
            {
                cmd.CommandText = @"
            UPDATE Cuotas 
            SET Pagado = 1 
            WHERE Id = @cuotaId AND NumeroSocio = @numeroSocio";
                cmd.Parameters.AddWithValue("@cuotaId", cuotaId);
                cmd.Parameters.AddWithValue("@numeroSocio", this.NumeroSocio);

                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    Cuota cuotaPagada = Cuotas.Find(c => c.Id == cuotaId);
                    if (cuotaPagada != null)
                        cuotaPagada.Pagado = true;
                }
            }
        }
        //calcular cuota
        public int CalcularCuotasAdeudadas()
        {
            DateTime hoy = DateTime.Now;
            int mesesAdeudados = ((hoy.Year - FechaRegistro.Year) * 12) + hoy.Month - FechaRegistro.Month;
            return mesesAdeudados;
        }


    }
}
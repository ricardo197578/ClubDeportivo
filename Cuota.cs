using System;

namespace ClubDeportivo
{
	public class Cuota
	{
		public int Id {get;set;}
		public int NumeroSocio { get; set; }
		public DateTime FechaPago {get; set;}
		public decimal Monto {get;set;}
		public bool Pagado {get;set;}

        /*
		
        falta implementar???
        public DateTime FechaVencimiento { get; set; }???
        public MetodoPago MetodoPago { get; set; }???
        
         
		 */
        public Cuota(int id , int numeroSocio,DateTime fechaPago,decimal monto,bool pagado)
		{
			Id = id;
			NumeroSocio = numeroSocio;
			FechaPago = fechaPago;
			Monto = monto;
			Pagado = pagado;
		}
	}	
}
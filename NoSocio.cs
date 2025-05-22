using System;

namespace ClubDeportivo
{
	public class NoSocio : Persona
	{
		public int NumeroNoSocio { get; set; }
		public DateTime FechaRegistro { get; set; }

		public NoSocio(string nombre, string apellido, string dni, int numeroNoSocio)
			: base(nombre, apellido, dni)
		{
			NumeroNoSocio = numeroNoSocio;
			//Actividades = new List<Actividad>();
			FechaRegistro = DateTime.Now;
		}
	}

}
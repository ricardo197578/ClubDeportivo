using System;

namespace ClubDeportivo
{

public abstract class Persona
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Dni { get; set; }
       

        protected Persona(string nombre, string apellido, string dni)
    {
        Nombre = nombre;
        Apellido = apellido;
        Dni = dni;
    }
}
}
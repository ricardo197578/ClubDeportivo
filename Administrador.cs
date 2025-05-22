using System;
using ClubDeportivo;

namespace ClubDeportivo
{

public class Administrador : Persona
{
    public string Usuario { get; set; }
    public string Clave { get; set; }

    public Administrador(string nombre, string apellido, string dni, string usuario, string clave)
        : base(nombre, apellido, dni)
    {
        Usuario = usuario;
        Clave = clave;
    }
}
}
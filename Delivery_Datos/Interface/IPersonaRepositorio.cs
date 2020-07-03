using Delivery_Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Datos.Interface
{
    public interface IPersonaRepositorio
    {
        Task<Persona> Agregar(Persona persona);
        Task<(bool resultado, Persona persona)> ValidarIncioDeSesion(string Telefono, string Password);
    }
}

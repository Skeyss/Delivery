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

        Task<bool> Actualizar(Persona persona,bool encriptarPassword,bool encriptarCodigoDeVerificacion,bool encriptarPasswordReset);

        Task<(bool status, Persona persona)> ObtenerPersona(string Telefono);

        Task<(bool status, Persona persona)> ObtenerPersonaPorId(int Id);

        Task<(bool status,bool existe)> ExisteTelefono(string Codigo);

        Task<(bool status,Persona persona)> ValidarIncioDeSesion(string Telefono, string Password);

        Task<(bool status, bool SePidioCambiarContrasenha, Persona persona)> ValidarIncioDePasswordReset(string Telefono, string Codigo);

        Task<(bool status, bool existe, bool verificado)> ValidarCodigoDeTelefono(string Telefono, string Codigo);
    }
}

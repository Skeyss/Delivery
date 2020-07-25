using Delivery_Datos.Interface;
using Delivery_Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Datos.Repositorio
{
    public class PersonaRepositorio:IPersonaRepositorio
    {
        private DeliveryContext _contexto;

        private readonly IPasswordHasher<Persona> _passwordHasher;

        public PersonaRepositorio(DeliveryContext contexto, IPasswordHasher<Persona> passwordHasher)
        {
            _contexto = contexto;
            _passwordHasher = passwordHasher;
        }


        public async Task<Persona> Agregar(Persona personaCrear)
        {
            try
            {
                personaCrear.Password = _passwordHasher.HashPassword(personaCrear, personaCrear.Password);
                personaCrear.CodigoDeVerificacion = _passwordHasher.HashPassword(personaCrear, personaCrear.CodigoDeVerificacion);
                personaCrear.PasswordReset = "";
                personaCrear.PasswordReset = _passwordHasher.HashPassword(personaCrear, personaCrear.PasswordReset);
                _contexto.Persona.Add(personaCrear);
                await _contexto.SaveChangesAsync();
                return personaCrear;
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return null;
            }
         
        }

        public async Task<bool> Actualizar(Persona persona, bool encriptarPassword, bool encriptarCodigoDeVerificacion,bool encriptarPasswordReset)
        {
            try
            {
                if (encriptarPassword)
                {
                    persona.Password = _passwordHasher.HashPassword(persona, persona.Password);
                }

                if (encriptarCodigoDeVerificacion)
                {
                    persona.CodigoDeVerificacion = _passwordHasher.HashPassword(persona, persona.CodigoDeVerificacion);
                }
                if (encriptarPasswordReset)
                {
                    persona.PasswordReset = _passwordHasher.HashPassword(persona, persona.PasswordReset);
                }

                _contexto.Persona.Attach(persona);
                _contexto.Entry(persona).State = EntityState.Modified;
                return await _contexto.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return false;
            }
        }

        public async Task<(bool status, Persona persona)> ObtenerPersona(string Telefono)
        {
            try
            {
                var persona = await _contexto.Persona.SingleOrDefaultAsync(c => c.Telefono == Telefono);
                return (true, persona);
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return (false, null);
            }
        }

        public async Task<(bool status, Persona persona)> ObtenerPersonaPorId(int Id)
        {
            try
            {
                var persona = await _contexto.Persona.SingleOrDefaultAsync(c => c.Id == Id);
                return (true, persona);
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return (false, null);
            }
        }

        public async Task<(bool status, Persona persona)> ValidarIncioDeSesion(string Telefono, string Password)
        {
            try
            {
                var PersonaBd = await _contexto.Persona.FirstOrDefaultAsync(u => u.Telefono == Telefono);
                if (PersonaBd != null)
                {

                    var resultado = _passwordHasher.VerifyHashedPassword(PersonaBd, PersonaBd.Password, Password);
                    if (resultado == PasswordVerificationResult.Success)
                    {
                        return (true, PersonaBd);
                    }
                    else
                    {
                        return (true, null);
                    }

                }
                else
                {
                    return (true, null);
                }
            }
            catch (Exception excepcion)
            {
                //Guardar mensaje ex
                return (false, null);
            }
        }

        public async Task<(bool status, bool SePidioCambiarContrasenha, Persona persona)> ValidarIncioDePasswordReset(string Telefono, string Codigo)
        {
            try
            {
                var PersonaBd = await _contexto.Persona.FirstOrDefaultAsync(u => u.Telefono == Telefono);
                if (PersonaBd != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(PersonaBd, PersonaBd.PasswordReset, "");

                    bool sePidioCambiarContrasenha = (result == PasswordVerificationResult.Success) ? false : true;

                    var resultado = _passwordHasher.VerifyHashedPassword(PersonaBd, PersonaBd.PasswordReset, Codigo);
                    if (resultado == PasswordVerificationResult.Success)
                    {
                        return (true, sePidioCambiarContrasenha, PersonaBd);
                    }
                    else
                    {
                        return (true, sePidioCambiarContrasenha, null);
                    }

                }
                else
                {
                    return (true, false ,null);
                }
            }
            catch (Exception excepcion)
            {
                //Guardar mensaje ex
                return (false,false, null);
            }
        }

        public async Task<(bool status,bool existe, bool verificado)> ValidarCodigoDeTelefono(string Telefono, string Codigo)
        {
            try
            {
                var PersonaBd = await _contexto.Persona.FirstOrDefaultAsync(u => u.Telefono == Telefono);
                if (PersonaBd != null)
                {

                    var resultado = _passwordHasher.VerifyHashedPassword(PersonaBd, PersonaBd.CodigoDeVerificacion, Codigo);
                    if (resultado == PasswordVerificationResult.Success)
                    {
                        PersonaBd.TelefonoVerificado = "SI";
                        _contexto.Persona.Attach(PersonaBd);
                        _contexto.Entry(PersonaBd).State = EntityState.Modified;
                        return await _contexto.SaveChangesAsync() > 0 ? (true, true,true) : (false, true, true);
                    }
                    else
                    {
                        return (true,true, false);
                    }

                }
                else
                {
                    return (true, false,false);
                }
            }
            catch (Exception excepcion)
            {
                //Guardar mensaje ex
                return (false,false, false);
            }
        }

        public async Task<(bool,bool)> ExisteTelefono(string numeroDeTelefono)
        {
            try
            {
                return (true,_contexto.Persona.Any(e => e.Telefono == numeroDeTelefono));
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return (false, false);
            }
         
        }

       
    }
}

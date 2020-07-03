using Delivery_Datos.Interface;
using Delivery_Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<(bool resultado, Persona persona)> ValidarIncioDeSesion(string Telefono, string Password)
        {
            var PersonaBd = await _contexto.Persona.FirstOrDefaultAsync(u => u.Telefono == Telefono);
            if (PersonaBd != null)
            {
                try
                {
                    var resultado = _passwordHasher.VerifyHashedPassword(PersonaBd, PersonaBd.Password, Password);
                    if (resultado == PasswordVerificationResult.Success)
                    {
                        return (true, PersonaBd);
                    }
                    else
                    {
                        return (false, PersonaBd);
                    }
                }
                catch (Exception excepcion)
                {

                }
            }

            return (false, null);
        }

        public async Task<Persona> Agregar(Persona personaCrear)
        {      
            try
            {
                personaCrear.Password = _passwordHasher.HashPassword(personaCrear, personaCrear.Password);
                _contexto.Persona.Add(personaCrear);
                await _contexto.SaveChangesAsync();

            }
            catch (Exception excepcion)
            {
                personaCrear = null;
            }
            return personaCrear;
        }
    }
}

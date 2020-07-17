using Delivery.Core.HttpClientGeneral;
using Delivery.Models;
using Delivery.Models.Inicio;
using Delivery.Views.Inicio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Delivery.Services
{
    public class PersonaService
    {
        public async static Task<(bool Status, string mensaje)> IncioDeSesionAsync(string Telefono, string Password)
        {
            //
            IncioDeSesion inicioDeSesion = new IncioDeSesion();
            inicioDeSesion.Telefono = Telefono;
            inicioDeSesion.Password = Password;
            string url = AppSettings.ApiUrl + "api/IncioDeSesion";
            var servicePost = await Post<IncioDeSesion>.PostEntidadService<PersonaLogin>(url, null, inicioDeSesion);         
            if (servicePost.Status)
            {
                var persona = servicePost.Entidad;
                try
                {
                    await SecureStorage.SetAsync(AppSettings.nameId, persona.Id.ToString());
                    //await SecureStorage.SetAsync(AppSettings.nameTipoDeDocumento, persona.tipo);
                    //await SecureStorage.SetAsync(AppSettings.nameNumeroDeDocumento, persona.numer);
                    await SecureStorage.SetAsync(AppSettings.nameDenominacion, persona.Denominacion);
                    await SecureStorage.SetAsync(AppSettings.nameTelefono, persona.Telefono);
                    await SecureStorage.SetAsync(AppSettings.nameEmail, string.IsNullOrEmpty(persona.Email) ? "" : persona.Email);
                    await SecureStorage.SetAsync(AppSettings.namePassword, Password);
                    await SecureStorage.SetAsync(AppSettings.nameAccess, persona.Token);

                
                    Preferences.Set(AppSettings.nameFrom, persona.ValidFrom.GetValueOrDefault());
                    Preferences.Set(AppSettings.nameTo, persona.ValidTo.GetValueOrDefault());


                }
                catch (Exception exception) 
                {
                    return (false, "El dispositivo no es compatible con la aplicación ");
                }

            }
            return (servicePost.Status, servicePost.Mensaje);
        }

        public async static Task<(bool Status,string mensaje)> CreacionUsuarioAsync(CreacionPersona persona)
        {
            
            string url = AppSettings.ApiUrl + "api/IncioDeSesion/Registrate";
            var servicePost = await Post<CreacionPersona>.PostService(url, null, persona);
            return servicePost;
        }

        //con autorizacion

        public async static Task<(bool Status, string mensaje)> VerificacionDePINAsync( string codigo)
        {
            var validador = await TokenValidator.VerificarToken();

            if (validador.Status)
            {
                PersonaVerificacionDeCodigo verificacion = new PersonaVerificacionDeCodigo();
                verificacion.Id = await AppKey.ConseguirId();
                verificacion.Telefono = await AppKey.ConseguirTelefono();
                verificacion.CodigoDeVerificacion = codigo;

                string url = AppSettings.ApiUrl + "api/Persona/" + verificacion.Id + "/VerificarCodigo";

                return await Post<PersonaVerificacionDeCodigo>.PostService(url, await AppKey.ConseguirAccess(), verificacion);
            }
            else
            {
                return (validador.Status, validador.Mensaje);
            }          
        }

        public async static Task<(bool Status, string mensaje)> VolverAEnviarCodigoDePINAsync()
        {
            var validador = await TokenValidator.VerificarToken();

            if (validador.Status)
            {
                string url = AppSettings.ApiUrl + "api/Persona/" + await AppKey.ConseguirId() + "/VolverAEnviarCodigo";
                return await Get<object>.GetService(url, await AppKey.ConseguirAccess());
            }
            else
            {
                return (validador.Status, validador.Mensaje);
            }
        }

    }
}

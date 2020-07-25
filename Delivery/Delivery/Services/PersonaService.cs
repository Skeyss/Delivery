using Delivery.Core;
using Delivery.Core.HttpClientGeneral;
using Delivery.Models;
using Delivery.Models.Inicio;
using Delivery.Models.Inicio.Reset;
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
                    //await SecureStorage.SetAsync(AppSettings.nameTipoDeDocumento, persona.);
                    //await SecureStorage.SetAsync(AppSettings.nameNumeroDeDocumento, persona.NUME);
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

        public async static Task<(bool Status, string mensaje)> VerificacionDePINAsync(string Telefono, string codigo)
        {

            PersonaVerificacionDeCodigo verificacion = new PersonaVerificacionDeCodigo();
            verificacion.Telefono = Telefono;
            verificacion.CodigoDeVerificacion = codigo;

            string url = AppSettings.ApiUrl + "api/IncioDeSesion/VerificarCodigo";

            return await Post<PersonaVerificacionDeCodigo>.PostService(url, null, verificacion);

        }

        public async static Task<(bool Status, string mensaje)> VolverAEnviarCodigoDePINAsync(string Telefono)
        {

            string url = AppSettings.ApiUrl + "api/IncioDeSesion/" + Telefono + "/VolverAEnviarCodigo";
            return await Get<object>.GetService(url, null);
        }


        public async static Task<(bool Status, string mensaje)> EnviarDePINResetContrasenhaAsync(string Telefono)
        {
            string url = AppSettings.ApiUrl + "api/IncioDeSesion/" + Telefono + "/Reset";
            return await Post<object>.PostService(url, null, null);
        }

        public async static Task<(bool Status,int Id,string Token, string mensaje)> VerificacionDePINResetContrasenhaAsync(string Telefono, string Codigo)
        {
            IncioDeSesionResetPassword incioDeSesionResetPassword = new IncioDeSesionResetPassword();
            incioDeSesionResetPassword.Telefono = Telefono;
            incioDeSesionResetPassword.PasswordReset = Codigo;
            string url = AppSettings.ApiUrl + "api/IncioDeSesion/ResetPassword";
            var servicePost = await Post<IncioDeSesionResetPassword>.PostEntidadService<PersonaLogin>(url, null, incioDeSesionResetPassword);
            if (servicePost.Status)
            {
                return (servicePost.Status, servicePost.Entidad.Id, Seguridad.Encriptar(servicePost.Entidad.Token, Telefono), servicePost.Mensaje);
            }
            else
            {
                return (servicePost.Status, 0, null, servicePost.Mensaje);
            }

        }





        //    con autorizacion

        public async static Task<(bool Status, string mensaje)> ResetPassword(string Telefono,int Id,string NuevoPassword, string Authen)
        {

            PersonaCambioDePassword personaCambioDePassword = new PersonaCambioDePassword();
            personaCambioDePassword.Id = Id;
            personaCambioDePassword.Password = NuevoPassword;
            string url = AppSettings.ApiUrl + "api/Persona/" + Id.ToString() + "/ResetContrasenha";
            string authen = Authen == null ? null : Seguridad.Desencriptar(Authen, Telefono);
            return await Post<PersonaCambioDePassword>.PostService(url, authen, personaCambioDePassword);

        }


        //          var validador = await TokenValidator.VerificarToken();

        //        if (validador.Status)
        //        {
        //        }
        //        else
        //        {
        //            return (validador.Status, validador.Mensaje);
        //        }


    }
}

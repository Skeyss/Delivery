using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Delivery.Services
{
    public class TokenValidator
    {
        public static async Task<(bool Status,string Mensaje)> VerificarToken()
        {
            try
            {
                var from = Preferences.Get(AppSettings.nameFrom, DateTime.Now);
                var to = Preferences.Get(AppSettings.nameTo, DateTime.Now);
                var now = DateTime.UtcNow;
                if (from < now && now < to)
                {
                    return (true, "El tiempo de token aun es valido ");
                }
                else
                {
                    var telefono = await SecureStorage.GetAsync(AppSettings.nameTelefono);
                    var password = await SecureStorage.GetAsync(AppSettings.namePassword);
                    var login = await PersonaService.IncioDeSesionAsync(telefono, password);

                    return (login.Status, login.mensaje);
                }
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return (false, "Error en la autentificación, por favor vuelva a intentar otra vez");
            }

        }
    }
}

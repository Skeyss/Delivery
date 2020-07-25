using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Delivery
{
    public class AppSettings
    {
        public static string ApiUrl = "https://deliveryapihuancayo.azurewebsites.net/";

        public static string nameId = "ID";
        public static string nameTipoDeDocumento = "Tipo de documento";
        public static string nameNumeroDeDocumento = "Numero de documento";
        public static string nameDenominacion = "Nombre";
        public static string nameTelefono = "Telefono";
        public static string nameEmail = "Correo";
        public static string namePassword = "Autentificacion";
        public static string nameAccess = "Access";
        public static string nameGuardarIncioDeSesion = "IncioDeSesionAutomatico";
        public static string nameTo = "To";
        public static string nameFrom= "From";
        public static string nameIncioDeSesionAutomatico = "IncioDeSesionAutomatico ";
    }

    public class AppKey
    {
        public async static Task<int> ConseguirId() 
        {
            try
            {
                string ID= await SecureStorage.GetAsync(AppSettings.nameId);

                return Convert.ToInt32(ID);
            }
            catch (Exception exception)
            {
                return 0;
            }
        }


        public async static Task<string> ConseguirDenominacion()
        {
            try
            {
                return await SecureStorage.GetAsync(AppSettings.nameDenominacion);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async static Task<string> ConseguirTelefono()
        {
            try
            {
                return await SecureStorage.GetAsync(AppSettings.nameTelefono);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async static Task<string> ConseguirEmail()
        {
            try
            {
                return await SecureStorage.GetAsync(AppSettings.nameEmail);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async static Task<string> ConseguirPassword()
        {
            try
            {
                return await SecureStorage.GetAsync(AppSettings.namePassword);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async static Task<string> ConseguirAccess()
        {
            try
            {
                return await SecureStorage.GetAsync(AppSettings.nameAccess);
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }

}

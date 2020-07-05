using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Delivery.Core.HttpClientGeneral
{
    public class VerificarConexion
    {
        public static bool ExisteConexionInternet() 
        {
            try
            {
                var sistema = Device.RuntimePlatform;
                if (sistema == "iOS" || sistema == "Android")
                {
                    if (Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi) == false && Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular) == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (sistema == "UWP")
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.None || Connectivity.NetworkAccess == NetworkAccess.Unknown)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

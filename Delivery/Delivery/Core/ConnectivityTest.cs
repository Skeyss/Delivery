using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Delivery.Core
{
    public class ConnectivityTest
    {

        public Views.Auxiliar.ConnectivityPage page { get; set; }

        public ConnectivityTest()
        {
            // Register for connectivity changes, be sure to unsubscribe when finished
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            try
            {
                var sistema = Device.RuntimePlatform;
                if (sistema == "iOS" || sistema == "Android")
                {
                    if (e.ConnectionProfiles.Contains(ConnectionProfile.WiFi) == false && e.ConnectionProfiles.Contains(ConnectionProfile.Cellular) == false)
                    {
                        if (page == null)
                        {
                            string TextMostrar = "Buscando red … " + "\r\n" + "Activar Wi-Fi - Datos móviles";
                            page = new Views.Auxiliar.ConnectivityPage(TextMostrar);
                            Application.Current.MainPage.Navigation.PushModalAsync(page);
                        }
                    }
                    else
                    {
                        if (page == null)
                        {

                        }
                        else
                        {
                            page = null;
                            Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                    }
                }
                else if (sistema == "UWP")
                {
                    if (e.NetworkAccess == NetworkAccess.None || e.NetworkAccess == NetworkAccess.Unknown)
                    {
                        if (page == null)
                        {
                            string TextMostrar = "Buscando red … " + "\r\n" + "Conéctese a red local";
                            page = new Views.Auxiliar.ConnectivityPage(TextMostrar);
                            Application.Current.MainPage.Navigation.PushModalAsync(page);
                        }
                    }
                    else
                    {
                        if (page == null)
                        {

                        }
                        else
                        {
                            page = null;
                            Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("", "Error Verificación de Conexión", "Ok");
            }
         
    
        }

  
    }
}

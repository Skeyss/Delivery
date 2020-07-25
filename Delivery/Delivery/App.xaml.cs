using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Delivery.Services;
using Delivery.Views;
using Delivery.Views.Inicio;
using Delivery.Core.HttpClientGeneral;

namespace Delivery
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public App()
        {


            InitializeComponent();
            Device.SetFlags(new string[] { "Expander_Experimental" });
            Device.SetFlags(new string[] { "IndicatorView_Experimental" });
            Device.SetFlags(new string[] { "Shapes_Experimental" });
            // Device.SetFlags(new string[] { "Shapes_Experimental" });

            //skeys ver donde poner esto y se sale con el boton de volver aTraz
            Core.ConnectivityTest asd = new Core.ConnectivityTest();


            bool IncioDeSesion = false;
            var buscar = SecureStorage.GetAsync(AppSettings.nameIncioDeSesionAutomatico);
            if (buscar.ToString() == "SI")
            {
                IncioDeSesion = true;
            }

            if (IncioDeSesion==false)
            {
                SecureStorage.RemoveAll();
                MainPage = new NavigationPage(new BienvenidaPage());
            }
            else
            {
                
            }
        }

        private object ConnectivityTest()
        {
            throw new NotImplementedException();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}

using Delivery.Models.Inicio;
using Delivery.Views.Inicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio
{
    public class BienvenidaPageViewModel: BaseViewModel
    {
        public IList<Bienvenida> Bienvenidas { get; set; }
        public INavigation Navigation;
        public ICommand CommandCrearCuenta { get; set; }
        public ICommand CommandIniciarSesion { get; set; }

        public BienvenidaPageViewModel(INavigation _Navigation)
        {
            Navigation = _Navigation;
            CommandCrearCuenta = new Command(NavegarCrearCuentaPage);
            CommandIniciarSesion = new Command(NavegarIncioDeSesion);


        }

        private  void NavegarCrearCuentaPage() 
        {
            try
            {
                Navigation.PushAsync(new CrearCuentaPage());
            }
            catch (Exception ex)
            {
                //skeys error
                Application.Current.MainPage.DisplayAlert("Error",ex.Message,"cancel");
            }

        }

        private void NavegarIncioDeSesion()
        {
            try
            {
                Navigation.PushAsync(new IniciarSesionPage());
            }
            catch (Exception ex)
            {
                //skeys error
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "cancel");
            }

        }

    }
}

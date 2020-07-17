using Delivery.Models.Inicio;
using Delivery.Views.Inicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio
{
    public class BienvenidaPageViewModel : BaseViewModel
    {
        public IList<Bienvenida> Bienvenidas { get; set; }
        public INavigation Navigation;
        public ICommand CommandCrearCuenta { get; set; }
        public ICommand CommandIniciarSesion { get; set; }

        private CrearCuentaPage crearCuentaPage = new CrearCuentaPage();
        private bool activarBTnCrearCuenta;

        public bool ActivarBTnCrearCuenta
        {
            get => activarBTnCrearCuenta; set
            {
                activarBTnCrearCuenta = value;
                OnPropertyChanged();
            }
        }

        public BienvenidaPageViewModel(INavigation _Navigation)
        {
            Navigation = _Navigation;
            CommandCrearCuenta = new Command(NavegarCrearCuentaPage);
            CommandIniciarSesion = new Command(NavegarIncioDeSesion);
            // ActivarBTnCrearCuenta = true;
            ActivarBTnCrearCuenta = false;


        }

        private async void NavegarCrearCuentaPage()
        {

            try
            {
                //if (ActivarBTnCrearCuenta == true)
                //{
                //    ActivarBTnCrearCuenta = false;
                IsBusy = true;
                    await Navigation.PushAsync(crearCuentaPage, true);
                IsBusy = false;
                //    ActivarBTnCrearCuenta = true;
                //}

            }
            catch (Exception ex)
            {
                //skeys error
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "cancel");
            }

        }

        private async void NavegarIncioDeSesion()
        {
            try
            {
                await Navigation.PushAsync(new IniciarSesionPage());
            }
            catch (Exception ex)
            {
                //skeys error
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "cancel");
            }

        }

    }
}

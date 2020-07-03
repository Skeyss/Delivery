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


            //skeys mover esto a un get  de la base de datos
            List<Bienvenida> _pruebas = new List<Bienvenida>();



            Bienvenida bienvenida3 = new Bienvenida();
            bienvenida3.PathImagen = "https://lh3.googleusercontent.com/DXIyLCA8vtDhW2bmwTNm7vFJ7mUimu-kw1O9PErIMkNxQxW7fzd6eAI4HfIbbRn3CICv=w720-h310-rw";
            bienvenida3.MensajePrincipal = "Necesito el nombre de la aplicación";
            bienvenida3.MensajeSecundario = "También necesito los iconos";
            _pruebas.Add(bienvenida3);


            Bienvenida bienvenida4 = new Bienvenida();
            bienvenida4.PathImagen = "https://lh3.googleusercontent.com/mkH00YnITfQl9owRJ9lrQ0Ys6aMqpj3VBVOqRTFXHhxPQmtOB6M1BpR3QiWG5O05MXDc=w720-h310-rw";
            bienvenida4.MensajePrincipal = "Necesito el nombre de la aplicación";
            bienvenida4.MensajeSecundario = "También necesito los iconos";
            _pruebas.Add(bienvenida4);


            Bienvenida bienvenida7 = new Bienvenida();
            bienvenida7.PathImagen = "https://lh3.googleusercontent.com/DXIyLCA8vtDhW2bmwTNm7vFJ7mUimu-kw1O9PErIMkNxQxW7fzd6eAI4HfIbbRn3CICv=w720-h310-rw";
            bienvenida7.MensajePrincipal = "Necesito el nombre de la aplicación";
            bienvenida7.MensajeSecundario = "También necesito los iconos";
            _pruebas.Add(bienvenida7);

            Bienvenida bienvenida = new Bienvenida();
            bienvenida.PathImagen = "https://lh3.googleusercontent.com/mkH00YnITfQl9owRJ9lrQ0Ys6aMqpj3VBVOqRTFXHhxPQmtOB6M1BpR3QiWG5O05MXDc=w720-h310-rw";
            bienvenida.MensajePrincipal = "Necesito el nombre de la aplicación";
            bienvenida.MensajeSecundario = "También necesito los iconos";
            _pruebas.Add(bienvenida);

            Bienvenidas = _pruebas;
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

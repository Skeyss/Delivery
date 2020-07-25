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
        public List<Pantalladebienvenida> Bienvenidas { get; set; }
   
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


   
    

        public BienvenidaPageViewModel()
        {
            CommandCrearCuenta =
                                  new Command(
                                      execute: () =>
                                                   { 
                                                       NavegarCrearCuentaPage();
                                                       ((Command)CommandCrearCuenta).ChangeCanExecute();
                                                       ((Command)CommandIniciarSesion).ChangeCanExecute();
                                                   },
                                      canExecute: () => IsBusy != false
                                                ); 


            //MultiplyBy2Command = new Command(
            //                    execute: () =>
            //                    {
            //                        Number *= 2;
            //                        ((Command)MultiplyBy2Command).ChangeCanExecute();
            //                        ((Command)DivideBy2Command).ChangeCanExecute();
            //                    },
            //                       canExecute: () => Number < Math.Pow(2, 10));



            CommandIniciarSesion = new Command(NavegarIncioDeSesion);
            // ActivarBTnCrearCuenta = true;
            ActivarBTnCrearCuenta = false;

          

        }

        public async Task CargarDatosIniciales() 
        {
            
           

            try
            {

                IsBusy = true;
                var service = await Services.BienvenidaService.ConseguirPantallaDeBienvenida();
                IsBusy = false;
                if (service.Status)
                {
                    Bienvenidas = service.ListBienvenida;
                }
                else
                {
                    
                }

            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                await Application.Current.MainPage.DisplayAlert("", "Error en la aplicación, por favor vuelva a intentar otra vez", "Ok");
            }
        }

        private async void NavegarCrearCuentaPage()
        {

            try
            {
                if (CommandIniciarSesion.CanExecute(CommandIniciarSesion))
                {

                }
                else
                {

                }
                ////if (ActivarBTnCrearCuenta == true)
                ////{
                ////    ActivarBTnCrearCuenta = false;
                //IsBusy = true;
                //await Navigation.PushAsync(crearCuentaPage, true);
                //IsBusy = false;
                ////    ActivarBTnCrearCuenta = true;
                ////}
                ///
                await Application.Current.MainPage.Navigation.PushAsync(crearCuentaPage);

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
                await Application.Current.MainPage.Navigation.PushAsync(new IniciarSesionPage());

            }
            catch (Exception ex)
            {
                //skeys error
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "cancel");
            }

        }

    }
}

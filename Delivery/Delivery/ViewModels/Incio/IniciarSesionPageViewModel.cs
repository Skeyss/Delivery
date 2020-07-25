using Delivery.Views;
using Delivery.Views.Inicio.Reset;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio
{
    public class IniciarSesionPageViewModel:BaseViewModel
    {
        #region
        private string numeroDeTelefono;
        private string contrasenha;

        //[Required(ErrorMessage = "Campo obligatorio")]
        [MinLength(9, ErrorMessage = "Ingrese nueve dígitos")]
        public string NumeroDeTelefono
        {
            get
            {
                //Validacion(NumeroDeTelefono, () => numeroDeTelefono.StartsWith("9"), "Ingrese un número de Teléfono válido");
                return numeroDeTelefono;
            }

            set
            {
                numeroDeTelefono = value;
                Validacion(NumeroDeTelefono, () => numeroDeTelefono.Length > 0 ? numeroDeTelefono.StartsWith("9") : true, "Ingrese un número de Teléfono válido");
                OnPropertyChanged();
            }
        }

        [MinLength(6, ErrorMessage = "La contraseña debe de tener un mínimo de 6 caracteres")]
        public string Contrasenha
        {
            get => contrasenha;
            set
            {
                contrasenha = value;
                Validacion(Contrasenha, () => true, "");
                OnPropertyChanged();
            }
        }


        private void RevizarValidacionDeControles()
        {
            try
            {
                Validacion(NumeroDeTelefono == null ? "" : NumeroDeTelefono, () => true, "Ingrese un número de Teléfono válido", "NumeroDeTelefono");
                Validacion(Contrasenha == null ? "" : Contrasenha, () => true, "gg", "Contrasenha");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Error al validar información - ex", "Ok");
                //Guardar mensaje ex
            }
        }

        #endregion
        public ICommand CommandLogin  { get; set; }
        public ICommand CommandOlvidarContrasenha { get; set; }

        public IniciarSesionPageViewModel()
        {
            CommandLogin=new Command(Login);
            CommandOlvidarContrasenha = new Command(OlvidarContrasenha);
        }

        private async void  Login()
        {

            try
            {
                RevizarValidacionDeControles();
                if (HasErrors == false)
                {
                    IsBusy = true;
                    var InicioDeSesion = await Services.PersonaService.IncioDeSesionAsync(NumeroDeTelefono, Contrasenha);
                    IsBusy = false;

                    if (InicioDeSesion.Status)
                    {
                          Application.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", InicioDeSesion.mensaje, "Ok");
                    }

                }
                else
                {
                    //SE MUESTRAN LOS ERRORES MEDIANTE INOTIFCATION ERRORS 
                }
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                await Application.Current.MainPage.DisplayAlert("", "Error en la aplicación, por favor vuelva a intentar otra vez", "Ok");
            }
        }

        private async void OlvidarContrasenha ()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EnviarCodigoResetPage());
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                await Application.Current.MainPage.DisplayAlert("", "Error en la aplicación, por favor vuelva a intentar otra vez", "Ok");
            }
        }

    }
}

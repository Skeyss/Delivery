using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio.Reset
{
    public class ResetContrasenhaViewModel:BaseViewModel
    {

        #region
        private string contrasenhaUno;
        private string contrasenhaDos;

   

        [MinLength(6, ErrorMessage = "La contraseña debe de tener un mínimo de 6 caracteres")]
        public string ContrasenhaUno
        {
            get => contrasenhaUno;
            set
            {
                contrasenhaUno = value;
                Validacion(ContrasenhaUno, () => true, "");
                OnPropertyChanged();
            }
        }

        [MinLength(6, ErrorMessage = "La contraseña debe de tener un mínimo de 6 caracteres")]
        public string ContrasenhaDos
        {
            get => contrasenhaDos;
            set
            {
                contrasenhaDos = value;
                Validacion(ContrasenhaDos, () => (contrasenhaUno== ContrasenhaDos), "Las contraseñas tienen que ser iguales");
                OnPropertyChanged();
            }
        }


        private void RevizarValidacionDeControles()
        {
            try
            {
                Validacion(ContrasenhaUno == null ? "" : ContrasenhaUno, () => true, "gg", "ContrasenhaUno");
                Validacion(ContrasenhaDos == null ? "" : ContrasenhaDos, () => (contrasenhaUno == ContrasenhaDos), "Las contraseñas tienen que ser iguales", "ContrasenhaDos");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Error al validar información - ex", "Ok");
                //Guardar mensaje ex
            }
        }

        #endregion

        public int Id { get; set; }
        public string Telefono { get; set; }
        public string Authen { get; set; }
        public ICommand CommandCambiarContrasenha { get; set; }



        public ResetContrasenhaViewModel()
        {
            CommandCambiarContrasenha = new Command(CambiarPassword);
        }

        private async void CambiarPassword()
        {


            try
            {
                RevizarValidacionDeControles();
                if (HasErrors == false)
                {
                    IsBusy = true;

                    var service = await Services.PersonaService.ResetPassword(Telefono, Id, contrasenhaDos, Authen);
                    IsBusy = false;
                    if (service.Status)
                    {
                        await Application.Current.MainPage.DisplayAlert("    ", "Ya esta", "Ok");
                        //await Application.Current.MainPage.Navigation.PushModalAsync(new VerificacionCodigoPage(personaAGuardar.Telefono, Seguridad.Encriptar(personaAGuardar.Password, personaAGuardar.Telefono)));
                    }
                    else
                    {
                        //enviar codig0o de verificacion
                        await Application.Current.MainPage.DisplayAlert("Error", service.mensaje, "Ok");
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

    }
}

using Delivery.Views.Inicio.Reset;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio.CrearPersona.Reset
{
    public class EnviarCodigoResetPageViewModel:BaseViewModel
    {
        #region
        private string numeroDeTelefono;
    

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

      


        private void RevizarValidacionDeControles()
        {
            try
            {
                Validacion(NumeroDeTelefono == null ? "" : NumeroDeTelefono, () => true, "Ingrese un número de Teléfono válido", "NumeroDeTelefono");              
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Error al validar información - ex", "Ok");
                //Guardar mensaje ex
            }
        }

        #endregion

        public ICommand CommandVerificarNumero { get; set; }

        public EnviarCodigoResetPageViewModel()
        {
            CommandVerificarNumero = new Command(VerificarNumero);
        }


        private async void VerificarNumero()
        {


            try
            {
                RevizarValidacionDeControles();
                if (HasErrors == false)
                {
                    IsBusy = true;
                    var service = await Services.PersonaService.EnviarDePINResetContrasenhaAsync(NumeroDeTelefono);
                    IsBusy = false;
                    if (service.Status)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new VerificacionDeCodigoResetPage(NumeroDeTelefono));
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

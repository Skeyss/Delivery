using Delivery.Views.Inicio.Reset;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio.CrearPersona.Reset
{
    public class VerificacionDeCodigoResetPageViewModel:BaseViewModel
    {
        #region


        private int? codigoVerificar;
        private bool seEnvioCodigo;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese solo caracteres numéricos")]
        public int? CodigoVerificar
        {
            get => codigoVerificar; set
            {
                codigoVerificar = value;
                Validacion(CodigoVerificar, () => codigoVerificar == null ? true : codigoVerificar.ToString().Length == 6, "Ingrese los 6 dígitos del número PIN");
                OnPropertyChanged();
            }
        }


        private void RevizarValidacionDeControles()
        {
            try
            {
                Validacion(CodigoVerificar == null ? 0 : CodigoVerificar, () => codigoVerificar == null ? true : codigoVerificar.ToString().Length == 6, "Ingrese los 6 dígitos del número PIN", "CodigoVerificar");

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Error al validar información - ex", "Ok");
                //Guardar mensaje ex
            }
        }
        #endregion

        public string NumeroTelefonoAValidar { get; set; }

        public ICommand CommandVolverAEnviarCodigo { get; set; }
        public ICommand CommandVerificarCodigo { get; set; }

        public bool SeEnvioCodigo
        {
            get => seEnvioCodigo; set
            {
                seEnvioCodigo = value;
                OnPropertyChanged();
            }
        }

        public VerificacionDeCodigoResetPageViewModel()
        {
            CommandVolverAEnviarCodigo = new Command(VolverAEnviarCodigo);
            CommandVerificarCodigo = new Command(VerificarCodigo);
        }

        private async void VolverAEnviarCodigo()
        {
            try
            {
                SeEnvioCodigo = false;
                IsBusy = true;
                var service = await Services.PersonaService.EnviarDePINResetContrasenhaAsync(NumeroTelefonoAValidar);
                IsBusy = false;
                if (service.Status)
                {
                    SeEnvioCodigo = true;
                }
                else
                {
                    //enviar codig0o de verificacion
                    await Application.Current.MainPage.DisplayAlert("Error", service.mensaje, "Ok");
                }
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                await Application.Current.MainPage.DisplayAlert("", "Error en la aplicación, por favor vuelva a intentar otra vez", "Ok");
            }
        }

        private async void VerificarCodigo()
        {
            try
            {

                RevizarValidacionDeControles();
                if (HasErrors == false)
                {
                    IsBusy = true;
                    var service = await Services.PersonaService.VerificacionDePINResetContrasenhaAsync(NumeroTelefonoAValidar, codigoVerificar.ToString());
                    IsBusy = false;
                    if (service.Status)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new ResetContrasenha(NumeroTelefonoAValidar, service.Id, service.Token));
                    }
                    else
                    {
                        //enviar codig0o de verificacion
                        await Application.Current.MainPage.DisplayAlert("Error", service.mensaje, "Ok");
                    }
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

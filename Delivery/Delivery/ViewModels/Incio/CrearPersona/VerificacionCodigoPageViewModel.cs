using Delivery.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio.CrearPersona
{
    public class VerificacionCodigoPageViewModel : BaseViewModel
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
                Validacion(CodigoVerificar, () => codigoVerificar == null?true: codigoVerificar.ToString().Length==6, "Ingrese los 6 dígitos del número PIN");
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
        public string Contrasenha { get; set; }
        public ICommand TextchangedCommand { get; set; }
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

        public VerificacionCodigoPageViewModel()
        {
            //Task en ves de void TextchangedCommand = new Command(async (Texto) => await VerificarPIN((string)Texto));

            TextchangedCommand = new Command((Texto) => VerificarPIN((string)Texto));
            CommandVolverAEnviarCodigo = new Command(VolverAEnviarCodigo);
            CommandVerificarCodigo = new Command(VerificarCodigo);
        }

        private async void VolverAEnviarCodigo()
        {
            try
            {
                SeEnvioCodigo = false;
                IsBusy = true;
                var service = await Services.PersonaService.VolverAEnviarCodigoDePINAsync(NumeroTelefonoAValidar);
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

        private async void VerificarPIN(string TextoNumeroCuatro)
        {
            try
            {
                if (TextoNumeroCuatro != null && TextoNumeroCuatro.Length == 1)
                {
                    //RevizarValidacionDeControles();
                    //if (HasErrors == false)
                    //{
                    //    IsBusy = true;
                    //    string numero = numeroUno.ToString() + numeroDos.ToString() + numeroTres.ToString() + numeroCuatro.ToString();
                    //    var service = await Services.PersonaService.VerificacionDePINAsync(numero);
                    //    IsBusy = false;
                    //    if (service.Status)
                    //    {
                    //        await Application.Current.MainPage.DisplayAlert("OK", " Ya esta", "Ok");
                    //    }
                    //    else
                    //    {
                    //        //enviar codig0o de verificacion
                    //        await Application.Current.MainPage.DisplayAlert("Error", service.mensaje, "Ok");
                    //    }
                    //}
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
                    var service = await Services.PersonaService.VerificacionDePINAsync(NumeroTelefonoAValidar, codigoVerificar.ToString());
                    IsBusy = false;
                    if (service.Status)
                    {
                        var InicioDeSesion = await Services.PersonaService.IncioDeSesionAsync(NumeroTelefonoAValidar, Seguridad.Desencriptar(Contrasenha, NumeroTelefonoAValidar));
                        if (InicioDeSesion.Status)
                        {

                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", InicioDeSesion.mensaje, "Ok");
                        }

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

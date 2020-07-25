using Delivery.Core;
using Delivery.Models;
using Delivery.Models.Inicio;
using Delivery.Views.Inicio;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio.CrearPersona
{
    public class CrearCuentaPageViewModel : BaseViewModel
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
                Validacion(NumeroDeTelefono, () => numeroDeTelefono.Length>0? numeroDeTelefono.StartsWith("9"):true, "Ingrese un número de Teléfono válido");
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

        public ICommand CommandCrearCuenta { get; set; }

      

        public CrearCuentaPageViewModel()
        {
            CommandCrearCuenta = new Command(CrearCuenta);
            #region 
            ListCodigosDeTelefono = new List<CodigoTelefonoPais>();
            CodigoTelefonoPais pruebas = new CodigoTelefonoPais();
            ListCodigosDeTelefono.Add(pruebas);
            SelectedCodigo = pruebas;
            #endregion
        }   

        private async void CrearCuenta() 
        {
          

            try
            {
                RevizarValidacionDeControles();
                if (HasErrors == false)
                {
                    IsBusy = true;
                    CreacionPersona personaAGuardar = PasarDatosDeInterfazAEntidad();
                    var service = await Services.PersonaService.CreacionUsuarioAsync(personaAGuardar);
                    IsBusy = false;
                    if (service.Status)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new VerificacionCodigoPage(personaAGuardar.Telefono, Seguridad.Encriptar(personaAGuardar.Password, personaAGuardar.Telefono)));
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

        private CreacionPersona PasarDatosDeInterfazAEntidad()
        {


            CreacionPersona resultado = new CreacionPersona();
            resultado.Telefono = NumeroDeTelefono;
            resultado.Password = contrasenha;
            return resultado;
        }
     


















        #region

        public IList<CodigoTelefonoPais> ListCodigosDeTelefono { get; set; }

        CodigoTelefonoPais selectedCodigo;
        

        public CodigoTelefonoPais SelectedCodigo
        {
            get { return selectedCodigo; }
            set
            {
                if (selectedCodigo != value)
                {
                    selectedCodigo = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Validacion de formulario Data TRIGGRES
        //private string numeroDeTelefono;
        //private string nombre;
        //private string contrasenha;
        //private bool aceptoLosTerminosDeUso;
        //private bool isValid;

        //public string NumeroDeTelefono
        //{
        //    get => numeroDeTelefono; set
        //    {
        //        numeroDeTelefono = value;
        //        ValidarFormulario();
        //    }
        //}
        //public string Nombre
        //{
        //    get => nombre; set
        //    {
        //        nombre = value;
        //        ValidarFormulario();
        //    }
        //}
        //public string Contrasenha
        //{
        //    get => contrasenha; set
        //    {
        //        contrasenha = value;
        //        ValidarFormulario();
        //    }
        //}
        //public bool AceptoLosTerminosDeUso
        //{
        //    get => aceptoLosTerminosDeUso; set
        //    {
        //        aceptoLosTerminosDeUso = value;
        //        ValidarFormulario();

        //    }
        //}
        //public bool IsValid
        //{
        //    get => isValid; set
        //    {
        //        isValid = value;
        //        OnPropertyChanged();
        //    }
        //}

        //    //<Button.Triggers>
        //    //        <DataTrigger
        //    //            Binding = "{Binding IsValid}"
        //    //            TargetType="Button"
        //    //            Value="true"
        //    //            >
        //    //            <Setter Property = "IsVisible" Value="True"></Setter>
        //    //        </DataTrigger>
        //    //    </Button.Triggers>

        //       private void ValidarFormulario()
        //{
        //    try
        //    {
        //        if (
        //     string.IsNullOrEmpty(NumeroDeTelefono) == false &&
        //     NumeroDeTelefono.Length == 9 &&
        //     NumeroDeTelefono.StartsWith("9") &&
        //     string.IsNullOrEmpty(Nombre) == false &&
        //     (Nombre.Length > 5 && Nombre.Length < 101) &&
        //     string.IsNullOrEmpty(Contrasenha) == false &&
        //     (Contrasenha.Length > 6 && Contrasenha.Length < 25) &&
        //     AceptoLosTerminosDeUso == true
        //     )
        //        {
        //            IsValid = true;
        //        }
        //        else
        //        {
        //            IsValid = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //skeys seliminar
        //        Application.Current.MainPage.DisplayAlert("Error", ex.Message, "cancel");
        //    }

        //}
        #endregion


    }
}

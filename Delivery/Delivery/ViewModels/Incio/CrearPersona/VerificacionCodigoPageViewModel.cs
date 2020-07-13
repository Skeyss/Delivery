using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio.CrearPersona
{
    public class VerificacionCodigoPageViewModel : BaseViewModel
    {
        #region
        
        private int? numeroUno;
        private int? numeroDos;
        private int? numeroTres;
        private int? numeroCuatro;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un carácter valido ")]
        public int? NumeroUno
        {
            get => numeroUno; set
            {
                numeroUno = value;
                Validacion(NumeroUno, () => true, "");
                OnPropertyChanged();
            }
        }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un carácter valido ")]
        public int? NumeroDos
        {
            get => numeroDos; set
            {
                numeroDos = value;
                Validacion(NumeroDos, () => true, "");
                OnPropertyChanged();
            }
        }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un carácter valido ")]
        public int? NumeroTres
        {
            get => numeroTres; set
            {
                numeroTres = value;
                Validacion(NumeroTres, () => true, "");
                OnPropertyChanged();
            }
        }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un carácter valido ")]
        public int? NumeroCuatro
        {
            get => numeroCuatro; set
            {
                numeroCuatro = value;
                Validacion(NumeroCuatro, () => true, "");
                OnPropertyChanged();
            }
        }

        private void RevizarValidacionDeControles()
        {
            try
            {
                Validacion(NumeroUno == null ? 0 : NumeroUno, () => true, "la condicion es verdad nuca llega aqui", "NumeroUno");
                Validacion(NumeroDos == null ? 0 : NumeroDos, () => true, "la condicion es verdad nuca llega aqui", "NumeroDos");
                Validacion(NumeroTres == null ? 0 : NumeroTres, () => true, "la condicion es verdad nuca llega aqui", "NumeroTres");
                Validacion(NumeroCuatro == null ? 0 : NumeroCuatro, () => true, "la condicion es verdad nuca llega aqui", "NumeroCuatro");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Error al validar información - ex", "Ok");
                //Guardar mensaje ex
            }
        }
        #endregion

        public string  NumeroTelefonoAValidar { get; set; }

        public VerificacionCodigoPageViewModel()
        {

        }

     
    }
}

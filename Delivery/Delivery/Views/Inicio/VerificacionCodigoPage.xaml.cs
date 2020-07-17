using Delivery.ViewModels.Incio.CrearPersona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificacionCodigoPage : ContentPage
    {
        VerificacionCodigoPageViewModel verificacionCodigoPageViewModel = new VerificacionCodigoPageViewModel();

       
        public VerificacionCodigoPage(string NumeroDeTelefono)
        {
            InitializeComponent();
            verificacionCodigoPageViewModel.NumeroTelefonoAValidar = NumeroDeTelefono;
            this.BindingContext = verificacionCodigoPageViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.Opacity = 0;
            await this.FadeTo(1, 500, Easing.CubicIn);
                
            verificacionCodigoPageViewModel.ErrorsChanged += VerificacionCodigoPage_ErrorsChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            verificacionCodigoPageViewModel.ErrorsChanged -= VerificacionCodigoPage_ErrorsChanged;
        }


        private void VerificacionCodigoPage_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var propErrores = (verificacionCodigoPageViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
            var mensajeDeError = (verificacionCodigoPageViewModel.GetErrors(e.PropertyName) as List<string>);
            var propText = string.Join("\n", mensajeDeError);

            switch (e.PropertyName)
            {
                case nameof(verificacionCodigoPageViewModel.CodigoVerificar):
                    {
                        if (propErrores)
                        {
                            lblErrorCodigo.IsVisible = true;
                            lblErrorCodigo.Text = propText;
                        }
                        else
                        {
                            lblErrorCodigo.IsVisible = false;
                            lblErrorCodigo.Text = "";
                        }

                        break;
                    }
            
                default:
                    {
                        break;
                    }
            }

            //if (crearCuentaPageViewModel.HasErrors)
            //{
            //    btnCrearCuenta.IsEnabled = false;
            //}
            //else
            //{
            //    btnCrearCuenta.IsEnabled = true;
            //}

        }

       

        //private void entryNumeroUno_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue!=null)
        //    {
        //        if (e.NewTextValue.Length==1)
        //        {
        //            entryNumeroDos.Focus();
        //            entryNumeroDos.CursorPosition = 1;
        //        }
        //        else
        //        {
        //            entryNumeroUno.Focus();
        //        }
        //    }
        //}

        //private void entryNumeroDos_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue != null)
        //    {
                
        //        if (e.NewTextValue.Length == 1)
        //        {
        //            entryNumeroTres.Focus();
        //            entryNumeroTres.CursorPosition = 1;
        //        }
        //        else if (e.NewTextValue.Length == 0)
        //        {
        //            entryNumeroUno.Focus();
        //            entryNumeroUno.CursorPosition = 1;
        //        }
        //    }
        //}

        //private void entryNumeroTres_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue != null)
        //    {
        //        if (e.NewTextValue.Length == 1)
        //        {
        //            entryNumeroCuatro.Focus();
        //            entryNumeroCuatro.CursorPosition = 1;
        //        }
        //        else if (e.NewTextValue.Length == 0)
        //        {
        //            entryNumeroDos.Focus();
        //            entryNumeroDos.CursorPosition = 1;
        //        }
        //    }
        //}

        //private void entryNumeroCuatro_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue != null)
        //    {
        //        if (e.NewTextValue.Length == 0)
        //        {
        //            entryNumeroTres.Focus();
        //            entryNumeroTres.CursorPosition = 1;
        //        }
        //    }
        //}

        //private void entryNumeroUno_Completed(object sender, EventArgs e)
        //{

        //}
    }
}
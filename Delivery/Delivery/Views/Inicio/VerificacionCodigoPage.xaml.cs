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

       
        public VerificacionCodigoPage()
        {
            InitializeComponent();
            //verificacionCodigoPageViewModel.NumeroTelefonoAValidar = NumeroDeTelefono;
            this.BindingContext = verificacionCodigoPageViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
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
                case nameof(verificacionCodigoPageViewModel.NumeroUno):
                    {
                        if (propErrores)
                        {
                            frameUno.BorderColor = Color.Red;
                        }
                        else
                        {
                            frameUno.BorderColor = (Color)App.Current.Resources["ColorAuxiliarDos"];
                        }

                        break;
                    }
                case nameof(verificacionCodigoPageViewModel.NumeroDos):
                    {
                        if (propErrores)
                        {
                            frameDos.BorderColor = Color.Red;
                        }
                        else
                        {
                            frameDos.BorderColor = (Color)App.Current.Resources["ColorAuxiliarDos"];
                        }
                        break;
                    }
                case nameof(verificacionCodigoPageViewModel.NumeroTres):
                    {
                        if (propErrores)
                        {
                            frameTres.BorderColor = Color.Red;
                        }
                        else
                        {
                            frameTres.BorderColor = (Color)App.Current.Resources["ColorAuxiliarDos"];
                        }
                        break;
                    }
                case nameof(verificacionCodigoPageViewModel.NumeroCuatro):
                    {
                        if (propErrores)
                        {
                            frameCuatro.BorderColor = Color.Red;
                        }
                        else
                        {
                            frameCuatro.BorderColor = (Color)App.Current.Resources["ColorAuxiliarDos"];
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

       

        private void entryNumeroUno_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue!=null)
            {
                if (e.NewTextValue.Length==1)
                {
                    entryNumeroDos.Focus();
                    entryNumeroDos.CursorPosition = 1;
                }
                else
                {
                    entryNumeroUno.Focus();
                }
            }
        }

        private void entryNumeroDos_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                
                if (e.NewTextValue.Length == 1)
                {
                    entryNumeroTres.Focus();
                    entryNumeroTres.CursorPosition = 1;
                }
                else if (e.NewTextValue.Length == 0)
                {
                    entryNumeroUno.Focus();
                    entryNumeroUno.CursorPosition = 1;
                }
            }
        }

        private void entryNumeroTres_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                if (e.NewTextValue.Length == 1)
                {
                    entryNumeroCuatro.Focus();
                    entryNumeroCuatro.CursorPosition = 1;
                }
                else if (e.NewTextValue.Length == 0)
                {
                    entryNumeroDos.Focus();
                    entryNumeroDos.CursorPosition = 1;
                }
            }
        }

        private void entryNumeroCuatro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null)
            {
                if (e.NewTextValue.Length == 0)
                {
                    entryNumeroTres.Focus();
                    entryNumeroTres.CursorPosition = 1;
                }
            }
        }

        private void entryNumeroUno_Completed(object sender, EventArgs e)
        {

        }
    }
}
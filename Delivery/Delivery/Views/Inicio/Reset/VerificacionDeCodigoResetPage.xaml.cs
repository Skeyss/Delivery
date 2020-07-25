using Delivery.ViewModels.Incio.CrearPersona.Reset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views.Inicio.Reset
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificacionDeCodigoResetPage : ContentPage
    {
        VerificacionDeCodigoResetPageViewModel verificacionDeCodigoResetPageViewModel = new VerificacionDeCodigoResetPageViewModel();

        public VerificacionDeCodigoResetPage(string NumeroDeTelefono)
        {
            InitializeComponent();
            this.BindingContext = verificacionDeCodigoResetPageViewModel;
            verificacionDeCodigoResetPageViewModel.NumeroTelefonoAValidar = NumeroDeTelefono;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.Opacity = 0;
            await this.FadeTo(1, 500, Easing.CubicIn);

            verificacionDeCodigoResetPageViewModel.ErrorsChanged += VerificacionCodigoPage_ErrorsChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            verificacionDeCodigoResetPageViewModel.ErrorsChanged -= VerificacionCodigoPage_ErrorsChanged;
        }


        private void VerificacionCodigoPage_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var propErrores = (verificacionDeCodigoResetPageViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
            var mensajeDeError = (verificacionDeCodigoResetPageViewModel.GetErrors(e.PropertyName) as List<string>);
            var propText = string.Join("\n", mensajeDeError);

            switch (e.PropertyName)
            {
                case nameof(verificacionDeCodigoResetPageViewModel.CodigoVerificar):
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

        }
    }
}
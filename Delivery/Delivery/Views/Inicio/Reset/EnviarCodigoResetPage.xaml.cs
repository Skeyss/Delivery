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
    public partial class EnviarCodigoResetPage : ContentPage
    {
        EnviarCodigoResetPageViewModel enviarCodigoResetPageViewModel = new EnviarCodigoResetPageViewModel();
        public EnviarCodigoResetPage()
        {
            InitializeComponent();
            this.BindingContext = enviarCodigoResetPageViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            enviarCodigoResetPageViewModel.ErrorsChanged += CrearCuentaPageViewModel_ErrorsChanged;
        }

        private void CrearCuentaPageViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var propErrores = (enviarCodigoResetPageViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
            var mensajeDeError = (enviarCodigoResetPageViewModel.GetErrors(e.PropertyName) as List<string>);
            var propText = string.Join("\n", mensajeDeError);

            switch (e.PropertyName)
            {
                case nameof(enviarCodigoResetPageViewModel.NumeroDeTelefono):
                    {
                        if (propErrores)
                        {
                            lblErrorNumeroDeTelefono.IsVisible = true;
                            lblErrorNumeroDeTelefono.Text = propText;
                        }
                        else
                        {
                            lblErrorNumeroDeTelefono.IsVisible = false;
                            lblErrorNumeroDeTelefono.Text = "";
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            enviarCodigoResetPageViewModel.ErrorsChanged -= CrearCuentaPageViewModel_ErrorsChanged;
        }

    }
}
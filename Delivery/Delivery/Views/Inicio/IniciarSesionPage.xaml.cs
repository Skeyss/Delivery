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
    public partial class IniciarSesionPage : ContentPage
    {
        Delivery.ViewModels.Incio.IniciarSesionPageViewModel IniciarSesionPageViewModel = new Delivery.ViewModels.Incio.IniciarSesionPageViewModel();
        public IniciarSesionPage()
        {
            InitializeComponent();
            BindingContext = IniciarSesionPageViewModel;
        }

        protected async override void OnAppearing()
        {


            base.OnAppearing();
            IniciarSesionPageViewModel.ErrorsChanged += CrearCuentaPageViewModel_ErrorsChanged;



        }

        private void CrearCuentaPageViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var propErrores = (IniciarSesionPageViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
            var mensajeDeError = (IniciarSesionPageViewModel.GetErrors(e.PropertyName) as List<string>);
            var propText = string.Join("\n", mensajeDeError);

            switch (e.PropertyName)
            {
                case nameof(IniciarSesionPageViewModel.NumeroDeTelefono):
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
                case nameof(IniciarSesionPageViewModel.Contrasenha):
                    {
                        if (propErrores)
                        {
                            lblErrorContrasenha.IsVisible = true;
                            lblErrorContrasenha.Text = propText;
                        }
                        else
                        {
                            lblErrorContrasenha.IsVisible = false;
                            lblErrorContrasenha.Text = "";
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
            IniciarSesionPageViewModel.ErrorsChanged -= CrearCuentaPageViewModel_ErrorsChanged;
        }

    }
}
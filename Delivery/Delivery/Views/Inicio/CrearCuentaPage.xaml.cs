using Delivery.ViewModels.Incio.CrearPersona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace Delivery.Views.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCuentaPage : ContentPage
    {
        CrearCuentaPageViewModel crearCuentaPageViewModel = new CrearCuentaPageViewModel();

        public CrearCuentaPage()
        {
     
            InitializeComponent();            
            this.BindingContext = crearCuentaPageViewModel;
        }

        protected async override void OnAppearing()
        {
            

            base.OnAppearing();
            crearCuentaPageViewModel.ErrorsChanged += CrearCuentaPageViewModel_ErrorsChanged;

         

        }

        private void CrearCuentaPageViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var propErrores = (crearCuentaPageViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
            var mensajeDeError = (crearCuentaPageViewModel.GetErrors(e.PropertyName) as List<string>);
            var propText = string.Join("\n", mensajeDeError);

            switch (e.PropertyName)
            {
                case nameof(crearCuentaPageViewModel.NumeroDeTelefono):
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
                case nameof(crearCuentaPageViewModel.Contrasenha):
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
            crearCuentaPageViewModel.ErrorsChanged -= CrearCuentaPageViewModel_ErrorsChanged;
        }

     



    }

  
}
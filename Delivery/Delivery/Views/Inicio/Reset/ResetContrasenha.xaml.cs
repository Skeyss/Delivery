using Delivery.ViewModels.Incio.Reset;
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
    public partial class ResetContrasenha : ContentPage
    {
        ResetContrasenhaViewModel resetContrasenhaViewModel = new ResetContrasenhaViewModel();

        public ResetContrasenha(string Telefono,int Id,string Token)
        {
            InitializeComponent();
            resetContrasenhaViewModel.Telefono = Telefono;
            resetContrasenhaViewModel.Id = Id;
            resetContrasenhaViewModel.Authen = Token;
            this.BindingContext = resetContrasenhaViewModel;

        }

        protected async override void OnAppearing()
        {
            

            base.OnAppearing();
            resetContrasenhaViewModel.ErrorsChanged += CrearCuentaPageViewModel_ErrorsChanged;
       



        }

        private void CrearCuentaPageViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var propErrores = (resetContrasenhaViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
            var mensajeDeError = (resetContrasenhaViewModel.GetErrors(e.PropertyName) as List<string>);
            var propText = string.Join("\n", mensajeDeError);

            switch (e.PropertyName)
            {
                case nameof(resetContrasenhaViewModel.ContrasenhaUno):
                    {
                        if (propErrores)
                        {
                            lblErrorContrasenhaUno.IsVisible = true;
                            lblErrorContrasenhaUno.Text = propText;
                        }
                        else
                        {
                            lblErrorContrasenhaUno.IsVisible = false;
                            lblErrorContrasenhaUno.Text = "";
                        }

                        break;
                    }
                case nameof(resetContrasenhaViewModel.ContrasenhaDos):
                    {
                        if (propErrores)
                        {
                            lblErrorContrasenhaDos.IsVisible = true;
                            lblErrorContrasenhaDos.Text = propText;
                        }
                        else
                        {
                            lblErrorContrasenhaDos.IsVisible = false;
                            lblErrorContrasenhaDos.Text = "";
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            resetContrasenhaViewModel.ErrorsChanged -= CrearCuentaPageViewModel_ErrorsChanged;
        }
    }
}
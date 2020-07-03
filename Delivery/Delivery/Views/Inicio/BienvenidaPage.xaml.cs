using Delivery.ViewModels.Incio;
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
    public partial class BienvenidaPage : ContentPage
    {
        public BienvenidaPage()
        {
            InitializeComponent();
            BindingContext = new BienvenidaPageViewModel(Navigation);
        }

        private void btnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PushAsync(new CrearCuentaPage()) ;
        }
    }
}
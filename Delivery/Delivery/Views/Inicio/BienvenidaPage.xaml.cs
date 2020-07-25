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
        BienvenidaPageViewModel bienvenidaPageViewModel { get; set; }
        public  BienvenidaPage()
        {
            InitializeComponent();
            bienvenidaPageViewModel = new BienvenidaPageViewModel();
     
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (bienvenidaPageViewModel.Bienvenidas==null)
            {
                await bienvenidaPageViewModel.CargarDatosIniciales();
                BindingContext = bienvenidaPageViewModel;
            }

        }
    }
}
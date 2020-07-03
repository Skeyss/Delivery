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
        public IniciarSesionPage()
        {
            InitializeComponent();
            BindingContext = new Delivery.ViewModels.Incio.IniciarSesionPageViewModel(Navigation);
        }
    }
}
using Delivery.ViewModels.Busqueda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views.Busqueda
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgrupacionPage : ContentPage
    {
        public AgrupacionPage()
        {
            InitializeComponent();
            BindingContext = new AgrupacionPageViewModel(Navigation);
       
        }
     

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    StackLayout _StackLayout = (StackLayout)sender;
        //    _StackLayout.Scale = 1.5;
        //}
    }
}
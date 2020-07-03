using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views.Auxiliar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectivityPage : ContentPage
    {
        public string TextAMostrar = "Buscando red ...";

        public ConnectivityPage(string textoAMostrar)
        {
            InitializeComponent();
            TextAMostrar = textoAMostrar;
            BindingContext = TextAMostrar;
        }

        protected override bool OnBackButtonPressed()
        {
            //skeys ver esto
            return true;
        }


    }
}
using Delivery.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels.Incio
{
    public class IniciarSesionPageViewModel:BaseViewModel
    {

        public ICommand CommandLogin  { get; set; }
        public INavigation Navigation;
        public IniciarSesionPageViewModel(INavigation _Navigation)
        {
            CommandLogin=new Command(Login);
            Navigation = _Navigation;
        }

        private  void  Login()
        {

            try
            {
             //   Application.Current.MainPage = new MainPage();
               
            }
            catch (Exception ex)
            {
                //skeys error
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "cancel");
            }
        }

    }
}

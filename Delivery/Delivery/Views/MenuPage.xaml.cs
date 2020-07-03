using Delivery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;

        public ICommand NavigateCommand { get; private set; }

        public MenuPage()
        {
            InitializeComponent();



            NavigateCommand = new Command<Type>(
               (Type pageType) =>
               {

                   //Page page = (Page)Activator.CreateInstance(pageType);
                   //await Navigation.PushAsync(page);


                   var mainPageaa = Application.Current.MainPage as MasterDetailPage;
                   mainPageaa.Detail  = new NavigationPage((Page)Activator.CreateInstance(pageType));
                   mainPageaa.IsPresented = false;
                   //paginaPrincipal.Detail = new NavigationPage((Page)Activator.CreateInstance(pageType));
                   //paginaPrincipal.IsPresented = false;

               });

               BindingContext = this;


            //menuItems = new List<HomeMenuItem>
            //{
            //    new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse" },
            //    new HomeMenuItem {Id = MenuItemType.About, Title="About" }
            //};

            //ListViewMenu.ItemsSource = menuItems;
            //ListViewMenu.SelectedItem = menuItems[0];
            //ListViewMenu.ItemSelected += async (sender, e) =>
            //{
            //    if (e.SelectedItem == null)
            //        return;

            //    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
            //    await RootPage.NavigateFromMenu(id);
            //};
        }


 

    

    }
}
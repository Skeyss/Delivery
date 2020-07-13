using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.AAAAA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Header : ContentView
	{

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                "Title",
                typeof(string),
                typeof(Header),
                defaultValue: "Título",
                propertyChanged: TitleChanged);

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control =
                (Header)bindable;
            //control.lblTitle.Text = newValue.ToString();
        }

        public string Title
        {
            get
            {
                return GetValue(TitleProperty).ToString();
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }


        public Header ()
		{
			//InitializeComponent ();
		}
	}
}
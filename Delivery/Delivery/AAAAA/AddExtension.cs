using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;

namespace Delivery.AAAAA
{
    public class AddExtension : IMarkupExtension<string>
    {
        public int Numero1 { get; set; }
        public int Numero2 { get; set; }

        public string ProvideValue(IServiceProvider serviceProvider)
        {
            return (Numero1 + Numero2).ToString();
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<string>).ProvideValue(serviceProvider);
        }
    }
}

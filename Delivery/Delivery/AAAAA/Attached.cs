using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.AAAAA
{
    public class Attached
    {
        public static readonly
            BindableProperty IsAttachedProperty =
            BindableProperty.CreateAttached(
                "IsAttached",
                typeof(bool),
                typeof(Attached),
                false);

        public static bool GetIsAttached(BindableObject view)
        {
            return (bool)view.GetValue(IsAttachedProperty);
        }
        public static void SetIsAttached(BindableObject view, bool value)
        {
            view.SetValue(IsAttachedProperty, value);
        }
    }
}

//  local:OlderAttached.IsForOlderPeople="True"  xaml

    //if(Attached.GetIsAttached(lblTest))
    //        {
    //            DisplayAlert("Attached", "Property is attached", "OK");
    //        }
    //        else
    //        {
    //            DisplayAlert("NOT Attached", "Property is not attached", "OK");
    //        }
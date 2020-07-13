using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.AAAAA
{
    public class OlderAttached
    {
        public static readonly BindableProperty IsForOlderPeopleProperty =
            BindableProperty.CreateAttached(
                "IsForOlderPeople",
                typeof(bool),
                typeof(OlderAttached),
                false,
                propertyChanged: OnValueChanged); //cuadno tien esto se covnierte en behaivor

        private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable as Label != null)
            {
                var control = bindable as Label;
                control.FontSize = control.FontSize * 2;
                return;
            }
            if (bindable as Entry != null)
            {
                var control = bindable as Entry;
                control.FontSize = control.FontSize * 2;
                return;
            }
        }

        public static bool GetIsForOlderPeople(BindableObject obj)
        {
            return (bool)obj.GetValue(IsForOlderPeopleProperty);
        }

        public static void SetIsForOlderPeople(BindableObject obj, bool value)
        {
            obj.SetValue(IsForOlderPeopleProperty, value);
        }     

    }
}

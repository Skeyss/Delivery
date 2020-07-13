using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.AAAAA
{
    public class HealthBox : BoxView
    {
        public static readonly BindableProperty HealthStatusProperty =
            BindableProperty.Create(
                "HealthStatus",
                typeof(double),
                typeof(HealthBox),
                10.0,
                propertyChanged: ValueChanged);

        public HealthBox()
        {
            SetHealth((double)HealthStatusProperty.DefaultValue);
        }

        private static void ValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as HealthBox;
            if(instance !=null)
            {
                instance.SetHealth((double)newValue);
            }
        }

        void SetHealth(double health)
        {
            if(health < 4)
            {
                Color = Color.DarkRed;
            }
            else if(health >=4 && health < 8)
            {
                Color = Color.LightGoldenrodYellow;
            }
            else
            {
                Color = Color.DarkGreen;
            }
        }

        public double HealthStatus
        {
            get
            {
                return (double)GetValue(HealthStatusProperty);
            }
            set
            {
                SetValue(HealthStatusProperty, value);
            }
        }
    }
}

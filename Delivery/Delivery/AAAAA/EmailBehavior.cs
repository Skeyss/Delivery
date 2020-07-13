using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace BehaviorsDemo
{
    public class EmailBehavior : Behavior<Entry>
    {
        bool colorSet = false;
        Color color;

        #region Email
        public const string MatchEmailPattern =
    @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        public static bool IsEmail(string email)
        {
            if (email != null)
                return Regex.IsMatch(email, MatchEmailPattern);
            else
                return false;
        }
        #endregion

        public static readonly BindableProperty ErrorTextColorProperty =
            BindableProperty.Create("ErrorTextColor",
                typeof(Color),
                typeof(EmailBehavior),
                default(Color));        

        public Color ErrorTextColor
        {
            get { return (Color)GetValue(ErrorTextColorProperty); }
            set { SetValue(ErrorTextColorProperty, value); }
        }

        static readonly BindablePropertyKey IsValidPropertyKey =
            BindableProperty.CreateReadOnly("IsValid",
                typeof(bool),
                typeof(EmailBehavior),
                false);
        public static readonly BindableProperty IsValidProperty =
            IsValidPropertyKey.BindableProperty;        

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            private set { SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = IsEmail(e.NewTextValue);
            if(!colorSet)
            {
                colorSet = true;
                color = ((Entry)sender).TextColor;
            }
            ((Entry)sender).TextColor = IsValid ? color : ErrorTextColor;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Bindable_TextChanged;
        }
    }
}

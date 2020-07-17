using Delivery.Renderer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.Behavior
{
    public class MyEntryTextchangedBehavior : Behavior<MyEntry>
    {
        public static readonly BindableProperty TextchangedCommandProperty
            = BindableProperty.Create(
                "TextchangedCommand",
                typeof(ICommand),
                typeof(MyEntryTextchangedBehavior),
                null);



        public ICommand TextchangedCommand
        {
            get { return (ICommand)GetValue(TextchangedCommandProperty); }
            set { SetValue(TextchangedCommandProperty, value); }
        }


        protected override void OnAttachedTo(MyEntry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(MyEntry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextchangedCommand==null)
            {
                return;
            }

            if (TextchangedCommand.CanExecute(e.NewTextValue))
            {
                TextchangedCommand.Execute(e.NewTextValue);
            }
        }
    }
}

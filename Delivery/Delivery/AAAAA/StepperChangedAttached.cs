using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AttachedPropertiesDemo
{
    //envetos comados
    public class StepperChangedAttached
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached(
                "Command",
                typeof(ICommand),
                typeof(StepperChangedAttached),
                null,
                propertyChanged: OnStepperChanged);



        public static ICommand GetStepperChanged(BindableObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetStepperChanged(BindableObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        private static void OnStepperChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as Stepper;
            if(control!=null)
            {
                control.ValueChanged += Control_ValueChanged;
            }
        }

        private static void Control_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var control = sender as Stepper;
            var command = GetStepperChanged(control);
            if(command!=null && command.CanExecute(e.NewValue))
            {
                command.Execute(e.NewValue);
            }
        }
    }
}

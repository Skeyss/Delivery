using Delivery.Validaciones;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.Triggera
{
    public class ValidacionDeCorreoElectronico : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            bool esValido = Validaciones.Validaciones.IsValidEmail(sender.Text);

            if (esValido)
            {
                sender.BackgroundColor = Color.Red;
            }
            else
            {
                sender.BackgroundColor = Color.Black;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Delivery.Triggera
{
    public class ValidacionDeTelefonoCelular : TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            bool esValido = Validaciones.Validaciones.ValidarTelefonos7a10Digitos(sender.Text);

            if (esValido)
            {
                
            }
            else
            {

            }
        }
    }
}

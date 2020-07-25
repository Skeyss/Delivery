using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DeliveryWebApi.Services
{
    public class EnviarSMS
    {
        public  static void EnviarCodigoDeVerificacion(string phone,string mensaje ) 
        {
            try
            {
                //skeys mover a otra parte esto
                const string accountSid = "ACd8eedd2f1f449f6d1a28643268a172f9";
                const string authToken = "457d00f7aa1a7751e9bcc3cdf214a7b7";

                //const string accountSid = "AC937ab1ff3e112284ecd05b7f744fcf8c";
                //const string authToken = "1f217b5996af98cd0c308e652a7ff327";

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: mensaje, 
                    from: new Twilio.Types.PhoneNumber("+12017402360"),
                    to: new Twilio.Types.PhoneNumber("+51"+ phone)
                );
            }
            catch (Exception ex )
            {
                //Guardar mensaje ex
            }
     
        }
    }
}

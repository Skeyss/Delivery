using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Delivery_Dtos
{
    public class IncioDeSesionResetPassword
    {
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string PasswordReset { get; set; }
    }
}

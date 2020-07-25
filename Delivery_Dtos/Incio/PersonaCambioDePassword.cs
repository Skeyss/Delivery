using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Delivery_Dtos
{
    public class PersonaCambioDePassword
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "La contraseña tiene un minimo de 6 caracteres ")]
        [MaxLength(30, ErrorMessage = "La contraseña tiene un máximo de 30 caracteres ")]
        public string Password { get; set; }
    }
}

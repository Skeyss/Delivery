using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Delivery_Dtos
{
    public class PersonaCreacion
    {

        //[Required]
        //[MaxLength(100,ErrorMessage = "El nombre de la persona tiene un máximo de 100 caracteres")]
        //public string Denominacion { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15, ErrorMessage = "El Telefono de la persona tiene un máximo de 15 caracteres")]
        public string Telefono { get; set; }

        //[Required]
        //[MinLength(6, ErrorMessage = "La contraseña tiene un minimo de 6 caracteres ")]
        //[MaxLength(30, ErrorMessage = "La contraseña tiene un máximo de 30 caracteres ")]
        //public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Delivery_Dtos
{
    public class PersonaCreacion
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Denominacion { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15)]
        public string Telefono { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

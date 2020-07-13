using System;
using System.Collections.Generic;
using System.Text;

namespace Delivery_Dtos
{
    public class PersonaLogin
    {
        public int Id { get; set; }
        public string Denominacion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}

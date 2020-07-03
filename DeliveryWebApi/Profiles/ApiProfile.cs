using Delivery_Dtos;
using Delivery_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWebApi.Profiles
{
    public class ApiProfile: AutoMapper.Profile
    {
        public ApiProfile()
        {
            // this.CreateMap<Agrupacion, AgrupacionDto>().ReverseMap();

            this.CreateMap<string, bool>().ConvertUsing(str => str.ToUpper() == "SI" ? true : false);
            this.CreateMap<bool, string>().ConvertUsing(bollean => bollean ? "SI" : "NO");
            this.CreateMap<Agrupacion, AgrupacionDto>().ReverseMap();

            this.CreateMap<PersonaMostrar, Persona>().ReverseMap();
            this.CreateMap<PersonaCreacion, Persona>();
        }
    }
}

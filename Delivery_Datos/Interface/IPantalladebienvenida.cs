using Delivery_Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Datos.Interface
{
    public interface IPantalladebienvenida
    {
        Task<List<Pantalladebienvenida>> ObtenerTodoAsync();
    }
}

using Delivery_Datos.Core;
using Delivery_Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Datos.Interface
{
    public interface IAgrupacionRepositorio
    {
        Task<List<Agrupacion>> ObtenerAgrupacionesAsync();
        Task<bool> Existe(string Codigo);
        Task<Agrupacion> ObtenerAgrupacionAsync(string Codigo);
        Task<Agrupacion> Agregar(Agrupacion agrupacion);
        Task<bool> Actualizar(Agrupacion agrupacion);
        Task<bool> Eliminar(Agrupacion agrupacion);
    }
}

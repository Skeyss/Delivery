using Delivery.Core;
using Delivery.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.Interface
{
    public interface IAgrupacion
    {
        Task<EstadoDeEjecucion> AddAgrupacionAsync(Agrupacion agrupacion);
        Task<EstadoDeEjecucion> UpdateAgrupacionAsync(Agrupacion agrupacion);
        Task<EstadoDeEjecucion> DeleteAgrupacionAsync(string codigo);
        Task<EstadoDeConsulta> GetAgrupacionAsync(string codigo);
        Task<EstadoDeConsulta> GetAgrupacionesAsync(bool forceRefresh = false);
    }
}

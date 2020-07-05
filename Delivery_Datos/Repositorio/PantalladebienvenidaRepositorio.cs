using Delivery_Datos.Interface;
using Delivery_Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery_Datos.Repositorio
{
    public class PantalladebienvenidaRepositorio: IPantalladebienvenida
    {
        private DeliveryContext _contexto;

        public PantalladebienvenidaRepositorio(DeliveryContext contexto)
        {
            _contexto = contexto;
        }

   

        public async Task<List<Pantalladebienvenida>> ObtenerTodoAsync()
        {
            try
            {
                return await _contexto.Pantalladebienvenida.OrderBy(c => c.OrdenDeVisualizacion).ToListAsync();
            }

            catch (Exception exception)
            {
                return null;
            }
        }
    }
}

using Delivery_Datos.Interface;
using Delivery_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Delivery_Datos.Core;

namespace Delivery_Datos.Repositorio
{
    public class AgrupacionRepositorio : IAgrupacionRepositorio
    {
        private DeliveryContext _contexto;

        public AgrupacionRepositorio(DeliveryContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Agrupacion>> ObtenerAgrupacionesAsync()
        {
            try
            {
                return await _contexto.Agrupacion.OrderBy(c => c.Nombre).ToListAsync();
            }
            catch (Exception exception)
            {
                return null; 
            }
           
        }

        public async Task<bool> Existe(string Codigo)
        {
            try
            {
                return  _contexto.Agrupacion.Any(e => e.Codigo == Codigo);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Agrupacion> ObtenerAgrupacionAsync(string Codigo)
        {
            try
            {
                return await _contexto.Agrupacion.SingleOrDefaultAsync(c => c.Codigo == Codigo);
            }
            catch (Exception exception)
            {

                return null;
            }
           
        }

        public async Task<Agrupacion> Agregar(Agrupacion agrupacion)
        {          
            try
            {
                _contexto.Agrupacion.Add(agrupacion);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                agrupacion = null;
            }
            return agrupacion;
        }

        public async Task<bool> Actualizar(Agrupacion agrupacion)
        {           
            try
            {
                _contexto.Agrupacion.Attach(agrupacion);
                _contexto.Entry(agrupacion).State = EntityState.Modified;
                return await _contexto.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public async Task<bool> Eliminar(Agrupacion agrupacion)
        {
            try
            {
                _contexto.Agrupacion.Remove(agrupacion);
                return await _contexto.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

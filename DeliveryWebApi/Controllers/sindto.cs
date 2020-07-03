using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Delivery_Datos;
using Delivery_Entidades;
using Delivery_Datos.Interface;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

namespace DeliveryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sindto : ControllerBase
    {

        private IAgrupacionRepositorio _IAgrupacionRepositorio;
   
        public sindto(IAgrupacionRepositorio iAgrupacionRepositorio)
        {
            _IAgrupacionRepositorio = iAgrupacionRepositorio;
        }

        // GET: api/Agrupacion
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Agrupacion>>> Get()
        {
            var listResultado = await _IAgrupacionRepositorio.ObtenerAgrupacionesAsync();

            if (listResultado == null)
            {
                return BadRequest();
            }
            else
            {
                return listResultado;
            }

        }

        // GET: api/Agrupacion/5
        [HttpGet("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Agrupacion>> Get(string codigo)
        {
           
                var agrupacion = await _IAgrupacionRepositorio.ObtenerAgrupacionAsync(codigo);

                if (agrupacion==null)
                {
                    return NotFound();
                }
                else
                {
                    return agrupacion;
                }
     
        }

        // POST: api/Agrupacion
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Agrupacion>> Post(Agrupacion agrupacion)
        {
                Agrupacion nuevoProducto =await  _IAgrupacionRepositorio.Agregar(agrupacion);

                if (nuevoProducto==null)
                {
                    return BadRequest();
                }
                else
                {
                    return CreatedAtAction(nameof(Post), new { Codigo = nuevoProducto.Codigo, nuevoProducto });
                }         

        }

        // PUT: api/Agrupacion/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Agrupacion>> Put(string codigo,[FromBody] Agrupacion agrupacion)
        {

            if (codigo != agrupacion.Codigo)
            {
                return BadRequest();
            }

            if (agrupacion == null)
            {
                return BadRequest();
            }

            var agrupacionData = await _IAgrupacionRepositorio.ObtenerAgrupacionAsync(codigo);
            if (agrupacionData == null)
            {
                return NotFound();
            }
            else
            {
                var resultado = await _IAgrupacionRepositorio.Actualizar(agrupacion);
                if (resultado == false)
                {
                    return BadRequest();
                }
                else
                {
                    return agrupacion;
                }
            }


        }

        // PUT: api/Agrupacion/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPatch("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Agrupacion>> Put(string codigo, [FromBody] JsonPatchDocument<Agrupacion> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest();
                }


                var agrupacionActualizar = await _IAgrupacionRepositorio.ObtenerAgrupacionAsync(codigo);
                if (agrupacionActualizar == null)
                {
                    return NotFound();
                }
                else
                {
                    patchDocument.ApplyTo(agrupacionActualizar);

                    var resultado = await _IAgrupacionRepositorio.Actualizar(agrupacionActualizar);
                    if (resultado == false)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        return agrupacionActualizar;
                    }
                }
            }
            catch (Exception exception)
            {
                return BadRequest();
            }
   
        }

        // DELETE: api/Agrupacion/5
        [HttpDelete("{codigo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(string codigo)
        {
            var agrupacion = await _IAgrupacionRepositorio.ObtenerAgrupacionAsync(codigo);

            if (agrupacion == null)
            {
                return NotFound();
            }
            else
            {
                var resultado = await _IAgrupacionRepositorio.Eliminar(agrupacion);

                if (resultado == false)
                {
                    return BadRequest();
                }
                else
                {
                    return NoContent();
                }
            }

        }





    }
}

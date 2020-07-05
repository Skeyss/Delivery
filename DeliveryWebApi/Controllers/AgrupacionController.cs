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
using Delivery_Dtos;
using Microsoft.AspNetCore.Authorization;
  
namespace DeliveryWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AgrupacionController : ControllerBase
    {

        private IAgrupacionRepositorio _IAgrupacionRepositorio;
        private readonly IMapper _mapper;
        public AgrupacionController(IAgrupacionRepositorio iAgrupacionRepositorio,IMapper mapper)
        {
            _IAgrupacionRepositorio = iAgrupacionRepositorio;
            _mapper = mapper;
        }

        // GET: api/Agrupacion
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AgrupacionDto>>> Get()
        {
            var listResultado = await _IAgrupacionRepositorio.ObtenerAgrupacionesAsync();

            if (listResultado == null)
            {
                return BadRequest();
            }
            else
            {
                var listDto = _mapper.Map<List<AgrupacionDto>>(listResultado);
        

                return listDto;
            }
            //return listResultado;
        }

        // GET: api/Agrupacion/5
        [HttpGet("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AgrupacionDto>> Get(string codigo)
        {

            var agrupacion = await _IAgrupacionRepositorio.ObtenerAgrupacionAsync(codigo);

            if (agrupacion == null)
            {
                return NotFound();
            }
            else
            {
                var Dto = _mapper.Map<AgrupacionDto>(agrupacion);
                return Dto;
            }

        }

        // POST: api/Agrupacion
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AgrupacionDto>> Post(AgrupacionDto agrupacionDto)
        {
            var agrupacionGuardar = _mapper.Map<Agrupacion>(agrupacionDto);

            Agrupacion nuevoProductoGuardado = await _IAgrupacionRepositorio.Agregar(agrupacionGuardar);

            if (nuevoProductoGuardado == null)
            {
                return BadRequest();
            }
            else
            {
                AgrupacionDto Dto = _mapper.Map<AgrupacionDto>(nuevoProductoGuardado);
                return CreatedAtAction(nameof(Post), new { Codigo = Dto.Codigo, Dto });
            }

        }

        // PUT: api/Agrupacion/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AgrupacionDto>> Put(string codigo,[FromBody] AgrupacionDto agrupacionDto)
        {

            if (codigo != agrupacionDto.Codigo)
            {
                return BadRequest();
            }

            if (agrupacionDto == null)
            {
                return BadRequest();
            }

            bool existe = await _IAgrupacionRepositorio.Existe(codigo);

            if (existe == false)
            {
                return NotFound();
            }


            Agrupacion agrupacionGuardar = _mapper.Map<Agrupacion>(agrupacionDto);

            var resultado =  await _IAgrupacionRepositorio.Actualizar(agrupacionGuardar);
            if (resultado == false)
            {               
                return BadRequest();     
            }
            else
            {
                return agrupacionDto;
            }

        }

        // PUT: api/Agrupacion/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPatch("{codigo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AgrupacionDto>> Put(string codigo, [FromBody] JsonPatchDocument<AgrupacionDto> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest();
                }

                 
                //bool existe = await _IAgrupacionRepositorio.Existe(codigo);

                //if (existe == false)
                //{
                //    return NotFound();
                //}

                Agrupacion agrupacionActualizar = await _IAgrupacionRepositorio.ObtenerAgrupacionAsync(codigo);
                if (agrupacionActualizar == null)
                {
                    return NotFound();

                    
                }
                else
                 {
                    AgrupacionDto agrupacionDtoActualizar = _mapper.Map<AgrupacionDto>(agrupacionActualizar);

                    patchDocument.ApplyTo(agrupacionDtoActualizar);
                    return agrupacionDtoActualizar;

                    //agrupacionActualizar = _mapper.Map<Agrupacion>(agrupacionDtoActualizar);

                    //var resultado = await _IAgrupacionRepositorio.Actualizar(agrupacionActualizar);
                    //if (resultado == false)
                    //{
                    //    return BadRequest();
                    //}
                    //else
                    //{
                    //    return agrupacionDtoActualizar;
                    //}
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

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
using Microsoft.AspNetCore.Authorization;
using Delivery_Dtos;
using AutoMapper;

namespace DeliveryWebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private IPersonaRepositorio _IPersonaRepositorio;
        private readonly IMapper _mapper;

        public PersonaController(IPersonaRepositorio iPersonaRepositorio, IMapper mapper)
        {
            _IPersonaRepositorio = iPersonaRepositorio;
            _mapper = mapper;
        }

        // POST: api/Persona/Registrate
        [AllowAnonymous]
        [HttpPost("Registrate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonaMostrar>> Post([FromBody] PersonaCreacion personaCreacion)
        {

            if (personaCreacion==null)
            {
                return BadRequest();
            }

            Persona persona = _mapper.Map<Persona>(personaCreacion);
            
            if (persona.Denominacion.ToString().Trim()=="")
            {
                persona.Denominacion = persona.Telefono;
            }
            persona.TipoDeDocumentoCodigo = "0";

            var nuevoPersona = await _IPersonaRepositorio.Agregar(persona);
            if (nuevoPersona == null)
            {
                return BadRequest();
            }
            else
            {
                PersonaMostrar nuevoUsuarioDto = _mapper.Map<PersonaMostrar>(nuevoPersona);
                return CreatedAtAction(nameof(Post), new { id = nuevoUsuarioDto.Id }, nuevoUsuarioDto);
            }
        }

    }
}

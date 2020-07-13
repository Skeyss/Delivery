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
            try
            {                
                if (personaCreacion == null)
                {
                    return BadRequest("La solicitud es nula");
                }

                var resultadoDeBuscar = await _IPersonaRepositorio.ObtenerPersona(personaCreacion.Telefono);

                if (resultadoDeBuscar.status == false)
                {
                    return BadRequest("No se pudo verificar el número de teléfono, intente de nuevo por favor");
                }

                if (resultadoDeBuscar.persona==null)
                {
                    Persona personaAGuardar = _mapper.Map<Persona>(personaCreacion);
                    if (personaAGuardar.Denominacion.ToString().Trim() == "")
                    {
                        personaAGuardar.Denominacion = personaAGuardar.Telefono;
                    }
                    personaAGuardar.TelefonoVerificado = "NO";
                    personaAGuardar.TipoDeDocumentoCodigo = "0";
                    string codigoDeVerificacion = (new Random().Next(1000, 9999)).ToString();
                    personaAGuardar.CodigoDeVerificacion = codigoDeVerificacion;

                    //GuardarPersona
                    var nuevoPersona = await _IPersonaRepositorio.Agregar(personaAGuardar);
                    if (nuevoPersona == null)
                    {
                        return BadRequest("Error al intentar crear cuenta");
                    }
                    else
                    {
                        Services.EnviarSMS.EnviarCodigoDeVerificacion(personaAGuardar.Telefono, "Tu código de verificación de Pilco delivery es  " + codigoDeVerificacion);
                        PersonaMostrar nuevoUsuarioDto = _mapper.Map<PersonaMostrar>(nuevoPersona);
                        return CreatedAtAction(nameof(Post), new { id = nuevoUsuarioDto.Id }, nuevoUsuarioDto);
                    }
                }
                else
                {
                    if (resultadoDeBuscar.persona.TelefonoVerificado=="SI")
                    {
                        return BadRequest("El número de teléfono ya se encuentra registrado");
                    }
                    else
                    {
                        resultadoDeBuscar.persona.Denominacion = personaCreacion.Denominacion;
                        resultadoDeBuscar.persona.Password = personaCreacion.Password;
                        string codigoDeVerificacion= (new Random().Next(1000, 9999)).ToString();
                        resultadoDeBuscar.persona.CodigoDeVerificacion = codigoDeVerificacion;
                        //actualizarPersona

                        var statusActualizacion = await _IPersonaRepositorio.Actualizar(resultadoDeBuscar.persona, true, true);
                        if (statusActualizacion)
                        {
                            Services.EnviarSMS.EnviarCodigoDeVerificacion(resultadoDeBuscar.persona.Telefono, "Tu código de verificación de Pilco delivery es  " + codigoDeVerificacion);
                            PersonaMostrar nuevoUsuarioDto = _mapper.Map<PersonaMostrar>(resultadoDeBuscar.persona);
                            return CreatedAtAction(nameof(Post), new { id = nuevoUsuarioDto.Id }, nuevoUsuarioDto);
                        }
                        else
                        {
                            return BadRequest("Error al intentar crear cuenta");
                        }
                    }
                }
            }
            catch (Exception exception) 
            {
                //Guardar mensaje ex
                return BadRequest("Error inesperado al intentar crear cuenta");
            }
        }

    }
}

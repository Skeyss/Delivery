using AutoMapper;
using Delivery_Datos.Interface;
using Delivery_Dtos;
using Delivery_Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryWebApi.Controllers
{
    
   
    [Route("api/[controller]")]
    [ApiController]
    public class IncioDeSesionController : ControllerBase
    {
        private IPersonaRepositorio _IPersonaRepositorio;
        private TokenService _tokenService;
        private readonly IMapper _mapper;

        public IncioDeSesionController(IPersonaRepositorio _iPersonaRepositorio, TokenService tokenService, IMapper mapper)
        {
            _IPersonaRepositorio = _iPersonaRepositorio;
            _tokenService = tokenService;
            _mapper = mapper;
        }

       
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonaLogin>> Post([FromBody] IncioDeSesion incioDeSesion)
        {
            try
            {
                if (incioDeSesion == null)
                {
                    return BadRequest("Usuario/Contraseña Inválidos.");
                }

                var resultado = await _IPersonaRepositorio.ExisteTelefono(incioDeSesion.Telefono);
                if (resultado.status == false)
                {
                    return BadRequest("No se pudo verificar el número de teléfono, intente de nuevo por favor");
                }

                if (resultado.existe == false)
                {
                    return BadRequest("El número de teléfono no se encuentra registrado");

                }

                var resultadoValidacion = await _IPersonaRepositorio.ValidarIncioDeSesion(incioDeSesion.Telefono, incioDeSesion.Password);
                if (resultadoValidacion.status == false)
                {
                    return BadRequest("No se pudo verificar el número de teléfono, intente de nuevo por favor");
                }

                if (resultadoValidacion.persona == null)
                {
                    return BadRequest("Usuario/Contraseña Inválidos.");
                }

                var token = _tokenService.GenerarPersonaToken(resultadoValidacion.persona);

                PersonaLogin personaLogin = new PersonaLogin();
                personaLogin.Id = resultadoValidacion.persona.Id;
                personaLogin.Denominacion = resultadoValidacion.persona.Denominacion;
                personaLogin.Telefono = resultadoValidacion.persona.Telefono;
                personaLogin.Email = resultadoValidacion.persona.Email;
                personaLogin.Token = token.result;
                personaLogin.ValidFrom = token.notBeforesate;
                personaLogin.ValidTo = token.expiresDate;

                return personaLogin;
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return BadRequest("Error inesperado al intentar iniciar sesión ");
            }

        }


     
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

                if (resultadoDeBuscar.persona == null)
                {
                    Persona personaAGuardar = _mapper.Map<Persona>(personaCreacion);
                    personaAGuardar.Denominacion = personaAGuardar.Telefono;
                    personaAGuardar.TelefonoVerificado = "NO";
                    personaAGuardar.TipoDeDocumentoCodigo = "0";

                    string codigoDeVerificacion = (new Random().Next(100000, 999999)).ToString();
                    personaAGuardar.CodigoDeVerificacion = codigoDeVerificacion;
                    personaAGuardar.Password= codigoDeVerificacion;
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
                    if (resultadoDeBuscar.persona.TelefonoVerificado == "SI")
                    {
                        return BadRequest("El número de teléfono ya se encuentra registrado");
                    }
                    else
                    {
                        string codigoDeVerificacion = (new Random().Next(100000, 999999)).ToString();
                        resultadoDeBuscar.persona.CodigoDeVerificacion = codigoDeVerificacion;
                        resultadoDeBuscar.persona.Password = codigoDeVerificacion;
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

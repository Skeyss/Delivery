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
using Microsoft.AspNetCore.Connections.Features;

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
        [Authorize(Roles ="Persona")]
        [HttpPost("{Id}/VerificarCodigo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(int Id,[FromBody] PersonaVerificacionDeCodigo personaVerificacionDeCodigo)
        {
            try
            {

                if (
                    User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Persona"
                    &&
                    User.Claims.FirstOrDefault(X => X.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value != Id.ToString()
                    )
                {
                    return BadRequest("Acceso denegado");

                }

                if (personaVerificacionDeCodigo == null)
                {
                    return BadRequest("La solicitud es nula");
                }

                if (Id!=personaVerificacionDeCodigo.Id)
                {
                    return BadRequest("El usuario que envió es distinto al usuario que va a modificar");
                }




                var validar = await _IPersonaRepositorio.ValidarCodigoDeTelefono(personaVerificacionDeCodigo.Id, personaVerificacionDeCodigo.CodigoDeVerificacion);

                if (validar.status == false)
                {
                    return BadRequest("Ocurrió un problema al procesar la información. Intente de nuevo por favor");
                }

                if (validar.existe==false)
                {
                    return BadRequest("El usuario que intenta modificar no se encuentra registrado");
                }

                if (validar.verificado)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return BadRequest("El código ingresado es inválido");
                }

            }
            catch (Exception exception) 
            {
                //Guardar mensaje ex
                return BadRequest("Error inesperado al verificar código. Intente de nuevo por favor");
            }
        }

        [Authorize(Roles = "Persona")]
        [HttpGet("{Id}/VolverAEnviarCodigo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get(int Id)
        {
            try
            {

                if (
                    User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Persona"
                    &&
                    User.Claims.FirstOrDefault(X => X.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value != Id.ToString()
                    )
                {
                    return BadRequest("Acceso denegado");

                }

                var resultado = await _IPersonaRepositorio.ObtenerPersonaPorId(Id);

                if (resultado.status==false)
                {
                    return BadRequest("Ocurrió un problema al procesar la información. Intente de nuevo por favor");
                }

                if (resultado.persona==null)
                {
                    return BadRequest("El usuario que intenta modificar no se encuentra registrado ");
                }

                if (resultado.persona.TelefonoVerificado == "SI")
                {
                    return BadRequest("El número de teléfono ya se encuentra registrado, Por favor inicie sesión ");
                }
                else
                {
                    string codigoDeVerificacion = (new Random().Next(1000, 9999)).ToString();
                    resultado.persona.CodigoDeVerificacion = codigoDeVerificacion;

                    var statusActualizacion = await _IPersonaRepositorio.Actualizar(resultado.persona, false, true);
                    if (statusActualizacion)
                    {
                        Services.EnviarSMS.EnviarCodigoDeVerificacion(resultado.persona.Telefono, "Tu código de verificación de Pilco delivery es  " + codigoDeVerificacion);
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                    {
                        return BadRequest("Error al enviar código. Intente de nuevo por favor");
                    }
                }

            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return BadRequest("Error inesperado al enviar código. Intente de nuevo por favor");
            }
        }


    }
}

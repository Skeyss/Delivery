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

       


        [Authorize(Roles = "PersonaResetPassword")]
        [HttpPost("{Id}/ResetContrasenha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(int Id,[FromBody] PersonaCambioDePassword personaCambioDePassword)
        {
            try
            {

                if (
                    User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "PersonaResetPassword"
                    &&
                    User.Claims.FirstOrDefault(X => X.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value != Id.ToString()
                    )
                {
                    return BadRequest("Acceso denegado");

                }

                if (personaCambioDePassword==null)
                {
                    return BadRequest("La solicitud es nula");
                }

                if (Id != personaCambioDePassword.Id)
                {
                    return BadRequest("Solicitud invalida");                    
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

                if (resultado.persona.TelefonoVerificado != "SI")
                {
                    return BadRequest("El número de teléfono no se encuentra registrado");
                }
                else
                {
                    resultado.persona.Password = personaCambioDePassword.Password;
                    resultado.persona.PasswordReset = "";

                    var statusActualizacion = await _IPersonaRepositorio.Actualizar(resultado.persona, true, false,true);
                    if (statusActualizacion)
                    {
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                    {
                        return BadRequest("Error al actualizar contraseña. Intente de nuevo por favor ");
                    }
                }

            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return BadRequest("Error inesperado al procesar información  Intente de nuevo por favor");
            }
        }


    }
}

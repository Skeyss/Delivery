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

        public IncioDeSesionController(IPersonaRepositorio _iPersonaRepositorio, TokenService tokenService)
        {
            _IPersonaRepositorio = _iPersonaRepositorio;
            _tokenService = tokenService;
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


    }
}

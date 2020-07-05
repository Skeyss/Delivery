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
    [AllowAnonymous]
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

        //POST: api/sesion/login
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Post([FromBody] IncioDeSesion incioDeSesion)
        {
            var ASD = User.Claims;
            if (incioDeSesion==null)
            {
                return BadRequest("Usuario/Contraseña Inválidos.");
            }

            var resultadoValidacion = await _IPersonaRepositorio.ValidarIncioDeSesion(incioDeSesion.Telefono, incioDeSesion.Password);
            if (!resultadoValidacion.resultado)
            {
                return BadRequest("Usuario/Contraseña Inválidos.");
            }
            return _tokenService.GenerarPersonaToken(resultadoValidacion.persona);
        }


    }
}

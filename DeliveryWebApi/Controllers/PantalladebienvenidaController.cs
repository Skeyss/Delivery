using Delivery_Datos.Interface;
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
    public class PantalladebienvenidaController:ControllerBase
    {

        private IPantalladebienvenida _IPantalladebienvenida;
        private TokenService _tokenService;

        public PantalladebienvenidaController(IPantalladebienvenida _iPantalladebienvenida, TokenService tokenService)
        {
            _IPantalladebienvenida = _iPantalladebienvenida;
        }

        // GET: api/Pantalladebienvenida
        [HttpGet("Mostrar")]
        //skeys autoriazion
       // [Authorize(Roles = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Pantalladebienvenida>>> Get()
        {
            var listResultado = await _IPantalladebienvenida.ObtenerTodoAsync();
            if (listResultado == null)
            {
                return BadRequest();
            }
            else
            {
                return listResultado;
            }
        }

    }
}

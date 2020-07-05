using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryWebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
       
        [HttpGet("{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]     
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> Get(string value)
        {
            if (value=="Create")
            {
                return StatusCode(201);
            }
            if (value == "Bien")
            {
                return StatusCode(200);
            }
            if (value == "Bad")
            {
                return StatusCode(400);
            }
            if (value == "Not")
            {
                return StatusCode(404);
            }
            if (value == "Server")
            {
                return StatusCode(500);
            }

            return StatusCode(500);
        }

    }
}

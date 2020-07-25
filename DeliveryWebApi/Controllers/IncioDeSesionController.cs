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
        private IPantalladebienvenida _IPantalladebienvenida;
        private TokenService _tokenService;
        private readonly IMapper _mapper;

        public IncioDeSesionController(IPersonaRepositorio _iPersonaRepositorio, IPantalladebienvenida _iPantalladebienvenida, TokenService tokenService, IMapper mapper)
        {
            _IPersonaRepositorio = _iPersonaRepositorio;
            _IPantalladebienvenida = _iPantalladebienvenida;
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

                if (resultadoValidacion.persona.TelefonoVerificado != "SI")
                {
                    return BadRequest("El número de teléfono no se encuentra registrado");
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
                    personaAGuardar.Password= personaCreacion.Password;
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
                        resultadoDeBuscar.persona.Denominacion = resultadoDeBuscar.persona.Telefono;
                        resultadoDeBuscar.persona.TelefonoVerificado = "NO";
                        resultadoDeBuscar.persona.TipoDeDocumentoCodigo = "0";

                        string codigoDeVerificacion = (new Random().Next(100000, 999999)).ToString();
                        resultadoDeBuscar.persona.CodigoDeVerificacion = codigoDeVerificacion;
                        resultadoDeBuscar.persona.Password = personaCreacion.Password;
                        resultadoDeBuscar.persona.PasswordReset = "";
                        //actualizarPersona

                        var statusActualizacion = await _IPersonaRepositorio.Actualizar(resultadoDeBuscar.persona, true, true, true);
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


        // POST: api/Persona/Registrate
        [AllowAnonymous]
        [HttpPost("VerificarCodigo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] PersonaVerificacionDeCodigo personaVerificacionDeCodigo)
        {
            try
            {

                //if (
                //    User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Persona"
                //    &&
                //    User.Claims.FirstOrDefault(X => X.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value != Id.ToString()
                //    )
                //{
                //    return BadRequest("Acceso denegado");

                //}

                if (personaVerificacionDeCodigo == null)
                {
                    return BadRequest("La solicitud es nula");
                }

                //if (Id != personaVerificacionDeCodigo.Id)
                //{
                //    return BadRequest("El usuario que envió es distinto al usuario que va a modificar");
                //}




                var validar = await _IPersonaRepositorio.ValidarCodigoDeTelefono(personaVerificacionDeCodigo.Telefono, personaVerificacionDeCodigo.CodigoDeVerificacion);

                if (validar.status == false)
                {
                    return BadRequest("Ocurrió un problema al procesar la información. Intente de nuevo por favor");
                }

                if (validar.existe == false)
                {
                    return BadRequest("El número de teléfono no se encuentra registrado");
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

        [AllowAnonymous]
        [HttpGet("{Telefono}/VolverAEnviarCodigo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get(string Telefono)
        {
            try
            {

                //if (
                //    User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Persona"
                //    &&
                //    User.Claims.FirstOrDefault(X => X.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value != Id.ToString()
                //    )
                //{
                //    return BadRequest("Acceso denegado");

                //}
                if (Telefono == null)
                {
                    return BadRequest("La solicitud es nula");
                }

                var resultado = await _IPersonaRepositorio.ObtenerPersona(Telefono);

                if (resultado.status == false)
                {
                    return BadRequest("Ocurrió un problema al procesar la información. Intente de nuevo por favor");
                }

                if (resultado.persona == null)
                {
                    return BadRequest("El usuario que intenta modificar no se encuentra registrado ");
                }

                if (resultado.persona.TelefonoVerificado == "SI")
                {
                    return BadRequest("El número de teléfono ya se encuentra registrado, Por favor inicie sesión ");
                }
                else
                {
                    string codigoDeVerificacion = (new Random().Next(100000, 999999)).ToString();
                    resultado.persona.CodigoDeVerificacion = codigoDeVerificacion;

                    var statusActualizacion = await _IPersonaRepositorio.Actualizar(resultado.persona, false, true, false);
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


        #region

        [AllowAnonymous]
        [HttpPost("{Telefono}/Reset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(string Telefono)
        {
            try
            {
                if (Telefono == null)
                {
                    return BadRequest("La solicitud es nula");
                }

                var resultado = await _IPersonaRepositorio.ObtenerPersona(Telefono);

                if (resultado.status == false)
                {
                    return BadRequest("Ocurrió un problema al procesar la información. Intente de nuevo por favor");
                }

                if (resultado.persona == null)
                {
                    return BadRequest("El número de teléfono no se encuentra registrado");
                }

                if (resultado.persona.TelefonoVerificado != "SI")
                {
                    return BadRequest("El número de teléfono no se encuentra registrado");
                }
                else
                {                   

                    string codigoDeVerificacion = (new Random().Next(100000, 999999)).ToString();
                    resultado.persona.PasswordReset = codigoDeVerificacion;

                    var statusActualizacion = await _IPersonaRepositorio.Actualizar(resultado.persona, false, false, true);
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

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonaLogin>> Post([FromBody] IncioDeSesionResetPassword incioDeSesion)
        {
            try
            {
                if (incioDeSesion == null)
                {
                    return BadRequest("Usuario/Código Inválidos.");
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

                var resultadoValidacion = await _IPersonaRepositorio.ValidarIncioDePasswordReset(incioDeSesion.Telefono, incioDeSesion.PasswordReset);

                if (resultadoValidacion.status == false)
                {
                    return BadRequest("No se pudo verificar el número de teléfono, intente de nuevo por favor");
                }

                if (resultadoValidacion.persona == null)
                {
                    return BadRequest("Código Inválido.");
                }

                if (resultadoValidacion.persona.TelefonoVerificado != "SI")
                {
                    return BadRequest("El número de teléfono no se encuentra registrado");
                }

                if (resultadoValidacion.SePidioCambiarContrasenha==false)
                {
                    return BadRequest("Acceso denegado");
                }

                resultadoValidacion.persona.PasswordReset = "";
                var statusActualizacion = await _IPersonaRepositorio.Actualizar(resultadoValidacion.persona, false, false, true);
                if (statusActualizacion)
                {
                    var token = _tokenService.GenerarPersonaResetPasswordToken(resultadoValidacion.persona);

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
                else
                {
                    return BadRequest("Error al verificar código. Intente de nuevo por favor");
                }


             
            }
            catch (Exception exception)
            {
                //Guardar mensaje ex
                return BadRequest("Error inesperado al verificar código");
            }

        }

        #endregion


        // GET: api/Agrupacion
        [AllowAnonymous]
        [HttpGet("PantallaDeBienvenida")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Pantalladebienvenida>>> Get()
        {
            try
            {
                var listResultado = await _IPantalladebienvenida.ObtenerTodoAsync();

                if (listResultado.Status==false)
                {
                    return BadRequest();
                }
                else
                {
                    return listResultado.List;
                }
            }

            catch (Exception exception)
            {
                //Guardar mensaje ex
                return BadRequest("Error inesperado al procesar información .Intente de nuevo por favor");
            }
      
        }


    }
}

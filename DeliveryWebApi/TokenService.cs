using Microsoft.Extensions.Configuration;
using Delivery_Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace DeliveryWebApi
{
    public class TokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string result,DateTime notBeforesate, DateTime expiresDate) GenerarPersonaToken(Persona persona)
        {
            //Accedemos a la sección JwtSettings del archivo appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            //Obtenemos la clave secreta guardada en JwtSettings:SecretKey
            string secretKey = jwtSettings.GetValue<string>("SecretKey");
            //Obtenemos el tiempo de vida en minutos del Jwt guardada en JwtSettings:MinutesToExpiration
            int minutes = jwtSettings.GetValue<int>("MinutesToExpiration");
            //Obtenemos el valor del emisor del token en JwtSettings:Issuer
            string issuer = jwtSettings.GetValue<string>("Issuer");
            //Obtenemos el valor de la audiencia a la que está destinado el Jwt en JwtSettings:Audience
            string audience = jwtSettings.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(secretKey);

            //Creamos nuestra lista de Claims, en este caso para el Username,
            //el Email y el Perfil del Usuario.
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, persona.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name,string.IsNullOrWhiteSpace(persona.Denominacion)?"": persona.Denominacion));
            claims.Add(new Claim(ClaimTypes.Email, string.IsNullOrWhiteSpace(persona.Email) ? "" : persona.Email));
            claims.Add(new Claim(ClaimTypes.MobilePhone, persona.Telefono));
            claims.Add(new Claim(ClaimTypes.Role, "Persona"));
           
            // Creamos el objeto JwtSecurityToken
             var token = new JwtSecurityToken(              
              issuer: issuer,
              audience: audience,
              claims: claims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(minutes),
              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );
            
            // Creamos una representación en cadena del Token JWT (Json Web Token)         
            return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidFrom, token.ValidTo);

        }

        public (string result, DateTime notBeforesate, DateTime expiresDate) GenerarPersonaResetPasswordToken(Persona persona)
        {
            //Accedemos a la sección JwtSettings del archivo appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            //Obtenemos la clave secreta guardada en JwtSettings:SecretKey
            string secretKey = jwtSettings.GetValue<string>("SecretKey");
            //Obtenemos el tiempo de vida en minutos del Jwt guardada en JwtSettings:MinutesToExpiration
            int minutes = jwtSettings.GetValue<int>("MinutesToExpiration");
            //Obtenemos el valor del emisor del token en JwtSettings:Issuer
            string issuer = jwtSettings.GetValue<string>("Issuer");
            //Obtenemos el valor de la audiencia a la que está destinado el Jwt en JwtSettings:Audience
            string audience = jwtSettings.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(secretKey);

            //Creamos nuestra lista de Claims, en este caso para el Username,
            //el Email y el Perfil del Usuario.
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, persona.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, string.IsNullOrWhiteSpace(persona.Denominacion) ? "" : persona.Denominacion));
            claims.Add(new Claim(ClaimTypes.Email, string.IsNullOrWhiteSpace(persona.Email) ? "" : persona.Email));
            claims.Add(new Claim(ClaimTypes.MobilePhone, persona.Telefono));
            claims.Add(new Claim(ClaimTypes.Role, "PersonaResetPassword"));

            // Creamos el objeto JwtSecurityToken
            var token = new JwtSecurityToken(
             issuer: issuer,
             audience: audience,
             claims: claims,
             notBefore: DateTime.UtcNow,
             expires: DateTime.UtcNow.AddMinutes(minutes),
             signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
           );

            // Creamos una representación en cadena del Token JWT (Json Web Token)         
            return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidFrom, token.ValidTo);

        }


        public string GenerarInicioDeSesionToken()
        {
            //Accedemos a la sección JwtSettings del archivo appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            //Obtenemos la clave secreta guardada en JwtSettings:SecretKey
            string secretKey = jwtSettings.GetValue<string>("SecretKey");
            //Obtenemos el tiempo de vida en minutos del Jwt guardada en JwtSettings:MinutesToExpiration
            int minutes = jwtSettings.GetValue<int>("MinutesToExpiration");
            //Obtenemos el valor del emisor del token en JwtSettings:Issuer
            string issuer = jwtSettings.GetValue<string>("Issuer");
            //Obtenemos el valor de la audiencia a la que está destinado el Jwt en JwtSettings:Audience
            string audience = jwtSettings.GetValue<string>("Audience");

            var key = Encoding.ASCII.GetBytes(secretKey);

            //Creamos nuestra lista de Claims, en este caso para el Username,
            //el Email y el Perfil del Usuario.
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "SKeys"));
            claims.Add(new Claim(ClaimTypes.Name, "David Rayder Ambrosio Gaspar"));
            claims.Add(new Claim(ClaimTypes.Email, "skeys@"));
            claims.Add(new Claim(ClaimTypes.MobilePhone, "666666666"));
            claims.Add(new Claim(ClaimTypes.Role, "Seguridad"));


            // Creamos el objeto JwtSecurityToken
            JwtSecurityToken token = new JwtSecurityToken(
              issuer: issuer,
              audience: audience,
              claims: claims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddYears(50),
              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            ); ;

            // Creamos una representación en cadena del Token JWT (Json Web Token)         
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}

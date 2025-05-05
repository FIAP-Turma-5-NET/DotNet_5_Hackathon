using FIAP_HealthMed.Application.Interfaces;
using FIAP_HealthMed.Domain.Entity;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;

namespace FIAP_HealthMed.Application.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretJWT = _configuration.GetSection("SecretJWT").Value;
           
            if (string.IsNullOrEmpty(secretJWT))
            {
                throw new InvalidOperationException("A chave SecretJWT não foi configurada.");
            }

            var chaveCriptografia = Encoding.ASCII.GetBytes(secretJWT);

            var tokenPropriedades = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.Name, usuario.Nome),
                            new Claim(ClaimTypes.Role, (usuario.Role - 1).ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chaveCriptografia),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenPropriedades);
            return tokenHandler.WriteToken(token);
        }
    }
}

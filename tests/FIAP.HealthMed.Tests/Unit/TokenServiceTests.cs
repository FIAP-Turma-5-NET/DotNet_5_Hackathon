using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;

using Microsoft.Extensions.Configuration;

using Moq;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FIAP.HealthMed.Tests.Unit
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TokenService _service;

        public TokenServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _service = new TokenService(_configurationMock.Object);
        }

        [Fact]
        public void GerarToken_DeveRetornarToken_QuandoConfiguracaoEstaCorreta()
        {
            // Arrange  
            var usuario = new Usuario
            {
                Nome = "João Silva",
                CPF = "123.456.789-00",
                DDD = "11",
                Telefone = "987654321",
                Email = "joao.silva@example.com",
                SenhaHash = "hashed-password",
                Role = Role.Medico
            };

           
            var secretJWT = "minha-chave-secreta-para-teste-123456";
            _configurationMock
                .Setup(c => c.GetSection("SecretJWT").Value)
                .Returns(secretJWT);

            // Act  
            var token = _service.GerarToken(usuario);

            // Assert  
            Assert.NotNull(token);
            Assert.IsType<string>(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GerarToken_DeveLancarExcecao_QuandoSecretJWTNaoEstaConfigurado()
        {
            // Arrange  
            var usuario = new Usuario
            {
                Nome = "João Silva",
                CPF = "123.456.789-00",
                DDD = "11",
                Telefone = "987654321",
                Email = "joao.silva@example.com",
                SenhaHash = "hashed-password",
                Role = Role.Medico
            };

            _configurationMock
                .Setup(c => c.GetSection("SecretJWT").Value)
                .Returns((string)null);

            // Act & Assert  
            var exception = Assert.Throws<InvalidOperationException>(() => _service.GerarToken(usuario));
            Assert.Equal("A chave SecretJWT não foi configurada.", exception.Message);
        }

        [Fact]
        public void GerarToken_DeveLancarExcecao_QuandoUsuarioEhNulo()
        {
            // Arrange  
            Usuario usuario = null;

            var secretJWT = "minha-chave-secreta-para-teste";
            _configurationMock
                .Setup(c => c.GetSection("SecretJWT").Value)
                .Returns(secretJWT);

            // Act & Assert  
            Assert.Throws<ArgumentNullException>(() => _service.GerarToken(usuario));
        }
    }

    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "O usuário não pode ser nulo.");
            }

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
                   new Claim(ClaimTypes.Role, ((int)usuario.Role).ToString())
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

using FIAP_HealthMed.Application.Enums;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Interfaces;
using FIAP_HealthMed.Application.Model.Auth;
using FIAP_HealthMed.Application.Service;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Enums;
using Moq;


namespace FIAP.HealthMed.Tests.Unit
{
    public class AuthApplicationServiceTests
    {
        private readonly Mock<ILoginStrategyResolver> _loginStrategyResolverMock;
        private readonly Mock<IUsuarioDomainService> _usuarioDomainServiceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly AuthApplicationService _authService;

        public AuthApplicationServiceTests()
        {
            _loginStrategyResolverMock = new Mock<ILoginStrategyResolver>();
            _usuarioDomainServiceMock = new Mock<IUsuarioDomainService>();
            _tokenServiceMock = new Mock<ITokenService>();
            _authService = new AuthApplicationService(
                _loginStrategyResolverMock.Object,
                _usuarioDomainServiceMock.Object,
                _tokenServiceMock.Object
            );
        }

        [Fact]
        public async Task AutenticarLoginAsync_ComEmailValido_DeveRetornarToken()
        {
            // Arrange
            var request = new AuthLoginModelRequest { Login = "teste@email.com", Senha = "senha123" };
            var usuario = new Usuario 
            { 
                Id = 1, 
                Nome = "Teste",
                Email = "teste@email.com", 
                CPF = "12345678900",
                DDD = "11",
                Telefone = "987654321",
                SenhaHash = "senha123",
                Role = Role.Paciente
            };
            var strategyMock = new Mock<ILoginStrategy>();
            
            strategyMock.Setup(s => s.ObterUsuarioAsync(request.Login))
                .ReturnsAsync(usuario);
            
            _loginStrategyResolverMock.Setup(r => r.Resolver(TipoLogin.Email))
                .Returns(strategyMock.Object);
            
            _usuarioDomainServiceMock.Setup(s => s.VerificarLogin(usuario, request.Senha))
                .Returns(true);
            
            _tokenServiceMock.Setup(t => t.GerarToken(usuario))
                .Returns("token_jwt");

            // Act
            var result = await _authService.AutenticarLoginAsync(request);

            // Assert
            Assert.Equal("token_jwt", result);
            strategyMock.Verify(s => s.ObterUsuarioAsync(request.Login), Times.Once);
            _usuarioDomainServiceMock.Verify(s => s.VerificarLogin(usuario, request.Senha), Times.Once);
            _tokenServiceMock.Verify(t => t.GerarToken(usuario), Times.Once);
        }

        [Fact]
        public async Task AutenticarLoginAsync_ComUsuarioNaoEncontrado_DeveLancarExcecao()
        {
            // Arrange
            var request = new AuthLoginModelRequest { Login = "teste@email.com", Senha = "senha123" };
            var strategyMock = new Mock<ILoginStrategy>();
            
            strategyMock.Setup(s => s.ObterUsuarioAsync(request.Login))
                .ReturnsAsync((Usuario)null);
            
            _loginStrategyResolverMock.Setup(r => r.Resolver(TipoLogin.Email))
                .Returns(strategyMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _authService.AutenticarLoginAsync(request)
            );
        }

        [Fact]
        public async Task AutenticarLoginAsync_ComSenhaInvalida_DeveLancarExcecao()
        {
            // Arrange
            var request = new AuthLoginModelRequest { Login = "teste@email.com", Senha = "senha123" };
            var usuario = new Usuario 
            { 
                Id = 1, 
                Nome = "Teste",
                Email = "teste@email.com", 
                CPF = "12345678900",
                DDD = "11",
                Telefone = "987654321",
                SenhaHash = "senha123",
                Role = Role.Paciente
            };
            var strategyMock = new Mock<ILoginStrategy>();
            
            strategyMock.Setup(s => s.ObterUsuarioAsync(request.Login))
                .ReturnsAsync(usuario);
            
            _loginStrategyResolverMock.Setup(r => r.Resolver(TipoLogin.Email))
                .Returns(strategyMock.Object);
            
            _usuarioDomainServiceMock.Setup(s => s.VerificarLogin(usuario, request.Senha))
                .Returns(false);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _authService.AutenticarLoginAsync(request)
            );
        }
    }
} 
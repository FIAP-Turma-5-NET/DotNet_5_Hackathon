using AutoMapper;

using FIAP_HealthMed.Application.Model.Usuario;
using FIAP_HealthMed.Application.Service;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Interface.Services;
using FIAP_HealthMed.Producer.Interface;
using Moq;

namespace FIAP.HealthMed.Tests.Unit
{
    public class UsuarioApplicationServiceTests
    {
        private readonly Mock<IUsuarioDomainService> _usuarioDomainServiceMock;
        private readonly Mock<IEspecialidadeDomainService> _especialidadeDomainServiceMock;
        private readonly Mock<IUsuarioProducer> _usuarioProducer;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UsuarioApplicationService _service;

        public UsuarioApplicationServiceTests()
        {
            _usuarioDomainServiceMock = new Mock<IUsuarioDomainService>();
            _especialidadeDomainServiceMock = new Mock<IEspecialidadeDomainService>();
            _usuarioProducer = new Mock<IUsuarioProducer>();
            _mapperMock = new Mock<IMapper>();

            _service = new UsuarioApplicationService(
                _usuarioDomainServiceMock.Object,
                _especialidadeDomainServiceMock.Object,
                _usuarioProducer.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task CadastrarUsuario_DeveRetornarMensagemDeSucesso_QuandoDadosSaoValidos()
        {
            // Arrange
            var request = new UsuarioModelRequest
            {
                Nome = "João Silva",
                CPF = "123.456.789-00",
                Email = "joao.silva@example.com",
                Senha = "senha123",
                DDD = "11",
                Telefone = "987654321",
                TipoUsuario = "Medico"
            };

            var usuario = new Usuario
            {
                Nome = request.Nome,
                CPF = request.CPF,
                Email = request.Email,
                SenhaHash = "hashed-password",
                DDD = request.DDD,
                Telefone = request.Telefone,
                Role = Role.Medico
            };

            _mapperMock
                .Setup(mapper => mapper.Map<Usuario>(request))
                .Returns(usuario);

            _usuarioDomainServiceMock
                .Setup(service => service.CadastrarAsync(usuario))
                .ReturnsAsync(1);

            // Act
            var resultado = await _service.CadastrarAsync(request);

            // Assert
            Assert.Equal("Usuário cadastrado com sucesso!", resultado);
            _usuarioDomainServiceMock.Verify(service => service.CadastrarAsync(usuario), Times.Once);
        }

        [Fact]
        public async Task ObterUsuarioPorId_DeveRetornarUsuario_QuandoUsuarioExiste()
        {
            // Arrange
            var usuarioId = 1;
            var usuario = new Usuario
            {
                Id = usuarioId,
                Nome = "João Silva",
                CPF = "123.456.789-00",
                Email = "joao.silva@example.com",
                SenhaHash = "hashed-password",
                DDD = "11",
                Telefone = "987654321",
                Role = Role.Medico
            };

            var response = new UsuarioModelResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                Email = usuario.Email,
                DDD = usuario.DDD,
                Telefone = usuario.Telefone,
                Role = usuario.Role
            };

            _usuarioDomainServiceMock
                .Setup(service => service.ObterPorIdAsync(usuarioId))
                .ReturnsAsync(usuario);

            _mapperMock
                .Setup(mapper => mapper.Map<UsuarioModelResponse>(usuario))
                .Returns(response);

            // Act
            var resultado = await _service.ObterPorId(usuarioId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuarioId, resultado.Id);
            Assert.Equal("João Silva", resultado.Nome);
        }

        [Fact]
        public async Task InserirEspecialidadesUsuario_DeveChamarServico_QuandoDadosSaoValidos()
        {
            // Arrange
            var usuarioId = 1;
            var especialidadeIds = new List<int> { 1, 2, 3 };

            _usuarioDomainServiceMock
                .Setup(service => service.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds))
                .Returns(Task.CompletedTask);

            // Act
            await _service.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds);

            // Assert
            _usuarioDomainServiceMock.Verify(service => service.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds), Times.Once);
        }       
    }
}
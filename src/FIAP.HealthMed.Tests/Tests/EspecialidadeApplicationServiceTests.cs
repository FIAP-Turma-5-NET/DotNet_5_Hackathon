using AutoMapper;

using FIAP_HealthMed.Application.Model.Especialidade;
using FIAP_HealthMed.Application.Service;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Services;

using Moq;

namespace FIAP.HealthMed.Tests.Unit
{
    public class EspecialidadeApplicationServiceTests
    {
        private readonly Mock<IEspecialidadeDomainService> _especialidadeDomainServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly EspecialidadeApplicationService _service;

        public EspecialidadeApplicationServiceTests()
        {
            _especialidadeDomainServiceMock = new Mock<IEspecialidadeDomainService>();
            _mapperMock = new Mock<IMapper>();
            _service = new EspecialidadeApplicationService(
                _especialidadeDomainServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task CadastrarEspecialidade_DeveRetornarMensagemSucesso()
        {
            // Arrange
            var request = new EspecialidadeModelRequest
            {
                Nome = "Cardiologia",
                ValorConsulta = 200.00m
            };
            var entity = new Especialidade
            {
                Nome = request.Nome, 
                ValorConsulta = request.ValorConsulta
            };
            var expectedMessage = "Especialidade cadastrada com sucesso!";

            _mapperMock.Setup(m => m.Map<Especialidade>(request))
                .Returns(entity);
            _especialidadeDomainServiceMock.Setup(s => s.CadastrarAsync(entity))
                .ReturnsAsync(expectedMessage);

            // Act
            var result = await _service.CadastrarEspecialidade(request);

            // Assert
            Assert.Equal(expectedMessage, result);
            _mapperMock.Verify(m => m.Map<Especialidade>(request), Times.Once);
            _especialidadeDomainServiceMock.Verify(s => s.CadastrarAsync(entity), Times.Once);
        }

        [Fact]
        public async Task ObterPorId_QuandoExisteEspecialidade_DeveRetornarEspecialidade()
        {
            // Arrange
            int id = 1;
            var entity = new Especialidade
            {
                Id = id,
                Nome = "Cardiologia",
                ValorConsulta = 200.00m
            };
            var expectedResponse = new EspecialidadeModelResponse
            {
                Id = id,
                Nome = "Cardiologia",
                ValorConsulta = 200.00m
            };

            _especialidadeDomainServiceMock.Setup(s => s.ObterPorIdAsync(id))
                .ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<EspecialidadeModelResponse>(entity))
                .Returns(expectedResponse);

            // Act
            var result = await _service.ObterPorId(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Id, result.Id);
            Assert.Equal(expectedResponse.Nome, result.Nome);
            _especialidadeDomainServiceMock.Verify(s => s.ObterPorIdAsync(id), Times.Once);
            _mapperMock.Verify(m => m.Map<EspecialidadeModelResponse>(entity), Times.Once);
        }

        [Fact]
        public async Task ObterPorId_QuandoNaoExisteEspecialidade_DeveRetornarNull()
        {
            // Arrange
            int id = 1;
            _especialidadeDomainServiceMock.Setup(s => s.ObterPorIdAsync(id))
                .ReturnsAsync((Especialidade)null);

            // Act
            var result = await _service.ObterPorId(id);

            // Assert
            Assert.Null(result);
            _especialidadeDomainServiceMock.Verify(s => s.ObterPorIdAsync(id), Times.Once);
            _mapperMock.Verify(m => m.Map<EspecialidadeModelResponse>(It.IsAny<Especialidade>()), Times.Never);
        }

        [Fact]
        public async Task ObterTodas_QuandoExistemEspecialidades_DeveRetornarLista()
        {
            // Arrange
            var entities = new List<Especialidade>
                {
                    new Especialidade { Id = 1, Nome = "Cardiologia" },
                    new Especialidade { Id = 2, Nome = "Neurologia" }
                };

            var expectedResponse = new List<EspecialidadeModelResponse>
                {
                    new EspecialidadeModelResponse { Id = 1, Nome = "Cardiologia" },
                    new EspecialidadeModelResponse { Id = 2, Nome = "Neurologia" }
                };

            _especialidadeDomainServiceMock.Setup(s => s.ObterTodasAsync())
                .ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<EspecialidadeModelResponse>>(entities))
                .Returns(expectedResponse);

            // Act
            var result = await _service.ObterTodas();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Count, result.Count());
            _especialidadeDomainServiceMock.Verify(s => s.ObterTodasAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<EspecialidadeModelResponse>>(entities), Times.Once);
        }

        [Fact]
        public async Task ObterTodas_QuandoNaoExistemEspecialidades_DeveRetornarListaVazia()
        {
            // Arrange
            var entities = new List<Especialidade>();
            var expectedResponse = new List<EspecialidadeModelResponse>();

            _especialidadeDomainServiceMock.Setup(s => s.ObterTodasAsync())
                .ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<EspecialidadeModelResponse>>(entities))
                .Returns(expectedResponse);

            // Act
            var result = await _service.ObterTodas();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _especialidadeDomainServiceMock.Verify(s => s.ObterTodasAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<EspecialidadeModelResponse>>(entities), Times.Once);
        }
    }
}

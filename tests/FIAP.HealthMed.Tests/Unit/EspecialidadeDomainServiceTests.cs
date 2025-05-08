using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Service;

using Moq;

namespace FIAP.HealthMed.Tests.Unit
{
    public class EspecialidadeDomainServiceTests
    {
        private readonly Mock<IEspecialidadeRepository> _especialidadeRepositoryMock;
        private readonly EspecialidadeDomainService _service;

        public EspecialidadeDomainServiceTests()
        {
            _especialidadeRepositoryMock = new Mock<IEspecialidadeRepository>();
            _service = new EspecialidadeDomainService(_especialidadeRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarAsync_DeveRetornarMensagemDeSucesso_QuandoCadastroBemSucedido()
        {
            // Arrange
            var especialidade = new Especialidade { Nome = "Cardiologia", ValorConsulta = 300 };
            _especialidadeRepositoryMock
                .Setup(repo => repo.CadastrarAsync(especialidade))
                .ReturnsAsync(1);

            // Act
            var resultado = await _service.CadastrarAsync(especialidade);

            // Assert
            Assert.Equal("Especialidade cadastrada com sucesso.", resultado);
        }

        [Fact]
        public async Task CadastrarAsync_DeveLancarExcecao_QuandoCadastroFalhar()
        {
            // Arrange
            var especialidade = new Especialidade { Nome = "Cardiologia", ValorConsulta = 300 };
            _especialidadeRepositoryMock
                .Setup(repo => repo.CadastrarAsync(especialidade))
                .ReturnsAsync(0);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CadastrarAsync(especialidade));
        }

        [Fact]
        public async Task ObterTodasAsync_DeveRetornarListaDeEspecialidades_QuandoExistiremEspecialidades()
        {
            // Arrange
            var especialidades = new List<Especialidade>
            {
                new Especialidade { Nome = "Cardiologia", ValorConsulta = 300 },
                new Especialidade { Nome = "Dermatologia", ValorConsulta = 200 }
            };

            _especialidadeRepositoryMock
                .Setup(repo => repo.ObterTodasAsync())
                .ReturnsAsync(especialidades);

            // Act
            var resultado = await _service.ObterTodasAsync();

            // Assert
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarEspecialidade_QuandoIdExistir()
        {
            // Arrange
            var especialidade = new Especialidade { Id = 1, Nome = "Cardiologia", ValorConsulta = 300 };
            _especialidadeRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(1))
                .ReturnsAsync(especialidade);

            // Act
            var resultado = await _service.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Cardiologia", resultado?.Nome);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarNull_QuandoIdNaoExistir()
        {
            // Arrange
            _especialidadeRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(1))
                .ReturnsAsync((Especialidade?)null);

            // Act
            var resultado = await _service.ObterPorIdAsync(1);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ObterPorIdsAsync_DeveRetornarEspecialidades_QuandoIdsValidos()
        {
            // Arrange
            var especialidades = new List<Especialidade>
            {
                new Especialidade { Id = 1, Nome = "Cardiologia", ValorConsulta = 300 },
                new Especialidade { Id = 2, Nome = "Dermatologia", ValorConsulta = 200 }
            };

            _especialidadeRepositoryMock
                .Setup(repo => repo.ObterPorIdsAsync(It.IsAny<IEnumerable<int?>>()))
                .ReturnsAsync(especialidades);

            // Act
            var resultado = await _service.ObterPorIdsAsync(new List<int?> { 1, 2 });

            // Assert
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task ObterPorIdsAsync_DeveRetornarListaVazia_QuandoIdsInvalidos()
        {
            // Arrange
            _especialidadeRepositoryMock
                .Setup(repo => repo.ObterPorIdsAsync(It.IsAny<IEnumerable<int?>>()))
                .ReturnsAsync(new List<Especialidade>());

            // Act
            var resultado = await _service.ObterPorIdsAsync(new List<int?>());

            // Assert
            Assert.Empty(resultado);
        }
    }
}

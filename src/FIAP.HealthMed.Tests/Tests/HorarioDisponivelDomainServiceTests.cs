using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Service;

using Moq;
namespace FIAP.HealthMed.Tests.Unit
{
    public class HorarioDisponivelDomainServiceTests
    {
        private readonly Mock<IHorarioDisponivelRepository> _horarioRepositoryMock;
        private readonly HorarioDisponivelDomainService _service;

        public HorarioDisponivelDomainServiceTests()
        {
            _horarioRepositoryMock = new Mock<IHorarioDisponivelRepository>();
            _service = new HorarioDisponivelDomainService(_horarioRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarHorariosAsync_DeveCadastrarHorarios_QuandoDadosSaoValidos()
        {
            // Arrange
            var medicoId = 1;
            var horarios = new List<DateTime> { DateTime.Now.AddHours(1), DateTime.Now.AddHours(2) };

            _horarioRepositoryMock
                .Setup(repo => repo.InserirHorariosAsync(It.IsAny<IEnumerable<HorarioDisponivel>>()))
                .ReturnsAsync(true);

            // Act
            await _service.CadastrarHorariosAsync(medicoId, horarios);

            // Assert
            _horarioRepositoryMock.Verify(repo => repo.InserirHorariosAsync(It.IsAny<IEnumerable<HorarioDisponivel>>()), Times.Once);
        }

        [Fact]
        public async Task CadastrarHorariosAsync_DeveLancarExcecao_QuandoCadastroFalhar()
        {
            // Arrange
            var medicoId = 1;
            var horarios = new List<DateTime> { DateTime.Now.AddHours(1), DateTime.Now.AddHours(2) };

            _horarioRepositoryMock
                .Setup(repo => repo.InserirHorariosAsync(It.IsAny<IEnumerable<HorarioDisponivel>>()))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CadastrarHorariosAsync(medicoId, horarios));
        }

        [Fact]
        public async Task ObterPorMedicoIdAsync_DeveRetornarHorarios_QuandoMedicoIdExistir()
        {
            // Arrange
            var medicoId = 1;
            var horarios = new List<HorarioDisponivel>
            {
                new HorarioDisponivel { Id = 1, MedicoId = medicoId, DataHora = DateTime.Now, Ocupado = false },
                new HorarioDisponivel { Id = 2, MedicoId = medicoId, DataHora = DateTime.Now.AddHours(1), Ocupado = true }
            };

            _horarioRepositoryMock
                .Setup(repo => repo.ObterPorMedicoIdAsync(medicoId))
                .ReturnsAsync(horarios);

            // Act
            var resultado = await _service.ObterPorMedicoIdAsync(medicoId);

            // Assert
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task EditarHorarioAsync_DeveEditarHorario_QuandoDadosSaoValidos()
        {
            // Arrange
            var horarioId = 1;
            var novoHorario = DateTime.Now.AddHours(2);

            _horarioRepositoryMock
                .Setup(repo => repo.AtualizarHorarioAsync(horarioId, novoHorario))
                .ReturnsAsync(true);

            // Act
            await _service.EditarHorarioAsync(horarioId, novoHorario);

            // Assert
            _horarioRepositoryMock.Verify(repo => repo.AtualizarHorarioAsync(horarioId, novoHorario), Times.Once);
        }

        [Fact]
        public async Task EditarHorarioAsync_DeveLancarExcecao_QuandoEdicaoFalhar()
        {
            // Arrange
            var horarioId = 1;
            var novoHorario = DateTime.Now.AddHours(2);

            _horarioRepositoryMock
                .Setup(repo => repo.AtualizarHorarioAsync(horarioId, novoHorario))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.EditarHorarioAsync(horarioId, novoHorario));
        }

        [Fact]
        public async Task RemoverHorarioAsync_DeveRemoverHorario_QuandoDadosSaoValidos()
        {
            // Arrange
            var horarioId = 1;

            _horarioRepositoryMock
                .Setup(repo => repo.RemoverHorarioAsync(horarioId))
                .ReturnsAsync(true);

            // Act
            await _service.RemoverHorarioAsync(horarioId);

            // Assert
            _horarioRepositoryMock.Verify(repo => repo.RemoverHorarioAsync(horarioId), Times.Once);
        }

        [Fact]
        public async Task RemoverHorarioAsync_DeveLancarExcecao_QuandoRemocaoFalhar()
        {
            // Arrange
            var horarioId = 1;

            _horarioRepositoryMock
                .Setup(repo => repo.RemoverHorarioAsync(horarioId))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RemoverHorarioAsync(horarioId));
        }
    }
}


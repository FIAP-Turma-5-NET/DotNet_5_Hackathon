using FIAP_HealthMed.Application.Model.HorarioDisponivel;
using FIAP_HealthMed.Application.Service;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;

using AutoMapper;
using Moq;

namespace FIAP.HealthMed.Tests.Unit
{
    public class HorarioDisponivelApplicationServiceTests
    {
        private readonly Mock<IHorarioDisponivelDomainService> _horarioDisponivelDomainServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly HorarioDisponivelApplicationService _service;

        public HorarioDisponivelApplicationServiceTests()
        {
            _horarioDisponivelDomainServiceMock = new Mock<IHorarioDisponivelDomainService>();
            _mapperMock = new Mock<IMapper>();
            _service = new HorarioDisponivelApplicationService(
                _horarioDisponivelDomainServiceMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task CadastrarHorarios_DeveChamarServicoComParametrosCorretos()
        {
            // Arrange
            int medicoId = 1;
            var horarios = new List<DateTime> { DateTime.Now, DateTime.Now.AddHours(1) };

            // Act
            await _service.CadastrarHorarios(medicoId, horarios);

            // Assert
            _horarioDisponivelDomainServiceMock.Verify(s => s.CadastrarHorariosAsync(medicoId, horarios), Times.Once);
        }

        [Fact]
        public async Task EditarHorario_DeveChamarServicoComParametrosCorretos()
        {
            // Arrange
            int horarioId = 1;
            var novoHorario = DateTime.Now.AddHours(2);

            // Act
            await _service.EditarHorario(horarioId, novoHorario);

            // Assert
            _horarioDisponivelDomainServiceMock.Verify(s => s.EditarHorarioAsync(horarioId, novoHorario), Times.Once);
        }

        [Fact]
        public async Task ObterHorarios_DeveRetornarListaDeHorarios()
        {
            // Arrange
            int medicoId = 1;
            var entities = new List<HorarioDisponivel>
                {
                    new() { Id = 1, MedicoId = medicoId, DataHora = DateTime.Now },
                    new () { Id = 2, MedicoId = medicoId, DataHora = DateTime.Now.AddHours(1) }
                };
            var expectedResponse = new List<HorarioDisponivelModelResponse>
                {
                    new () { Id = 1, DataHora = DateTime.Now },
                    new () { Id = 2, DataHora = DateTime.Now.AddHours(1) }
                };

            _horarioDisponivelDomainServiceMock.Setup(s => s.ObterPorMedicoIdAsync(medicoId))
                .ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<HorarioDisponivelModelResponse>>(entities))
                .Returns(expectedResponse);

            // Act
            var result = await _service.ObterHorarios(medicoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Count, result.Count());
            _horarioDisponivelDomainServiceMock.Verify(s => s.ObterPorMedicoIdAsync(medicoId), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<HorarioDisponivelModelResponse>>(entities), Times.Once);
        }

        [Fact]
        public async Task RemoverHorario_DeveChamarServicoComParametroCorreto()
        {
            // Arrange
            int horarioId = 1;

            // Act
            await _service.RemoverHorario(horarioId);

            // Assert
            _horarioDisponivelDomainServiceMock.Verify(s => s.RemoverHorarioAsync(horarioId), Times.Once);
        }
    }
}

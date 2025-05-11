using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Service;

using Moq;

namespace FIAP.HealthMed.Tests.Unit
{
    public class ConsultaDomainServiceTests
    {
        private readonly Mock<IConsultaRepository> _consultaRepositoryMock;
        private readonly Mock<IHorarioDisponivelRepository> _horarioRepositoryMock;
        private readonly Mock<IEspecialidadeRepository> _especialidadeRepositoryMock;
        private readonly ConsultaDomainService _service;

        public ConsultaDomainServiceTests()
        {
            _consultaRepositoryMock = new Mock<IConsultaRepository>();
            _horarioRepositoryMock = new Mock<IHorarioDisponivelRepository>();
            _especialidadeRepositoryMock = new Mock<IEspecialidadeRepository>();

            _service = new ConsultaDomainService(
                _consultaRepositoryMock.Object,
                _horarioRepositoryMock.Object,
                _especialidadeRepositoryMock.Object
            );
        }

        [Fact]
        public async Task AgendarConsultaAsync_DeveAgendarConsulta_QuandoDadosSaoValidos()
        {
            // Arrange
            var consulta = new Consulta
            {
                MedicoId = 1,
                DataHora = DateTime.Now.AddHours(1),
                EspecialidadeId = 1
            };

            var horarioDisponivel = new HorarioDisponivel
            {
                Id = 1,
                MedicoId = consulta.MedicoId,
                DataHora = consulta.DataHora,
                Ocupado = false
            };

            var especialidade = new Especialidade
            {
                Id = consulta.EspecialidadeId,
                Nome = "Cardiologia", 
                ValorConsulta = 200
            };

            _horarioRepositoryMock
                .Setup(repo => repo.ObterPorMedicoIdAsync(consulta.MedicoId))
                .ReturnsAsync(new List<HorarioDisponivel> { horarioDisponivel });

            _especialidadeRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(consulta.EspecialidadeId))
                .ReturnsAsync(especialidade);

            _consultaRepositoryMock
                .Setup(repo => repo.AgendarAsync(consulta))
                .ReturnsAsync(1);

            // Act
            var resultado = await _service.AgendarConsultaAsync(consulta);

            // Assert
            Assert.Equal("Consulta agendada com sucesso!", resultado);
            _horarioRepositoryMock.Verify(repo => repo.AtualizarHorarioAsync(horarioDisponivel.Id, horarioDisponivel.DataHora), Times.Once);
        }

        [Fact]
        public async Task CancelarConsultaAsync_DeveCancelarConsulta_QuandoDadosSaoValidos()
        {
            // Arrange
            var consultaId = 1;
            var usuarioId = 2;
            var justificativa = "Paciente indisponível";

            var consulta = new Consulta
            {
                Id = consultaId,
                PacienteId = usuarioId,
                Status = StatusConsulta.Pendente,
                DataHora = DateTime.Now 
            };

            _consultaRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(consultaId))
                .ReturnsAsync(consulta);

            _consultaRepositoryMock
                .Setup(repo => repo.AtualizarStatusAsync(consultaId, StatusConsulta.Cancelada, justificativa))
                .ReturnsAsync(true);

            // Act
            var resultado = await _service.CancelarConsultaAsync(consultaId, usuarioId, justificativa);

            // Assert
            Assert.Equal("Consulta cancelada com sucesso.", resultado);
        }

        [Fact]
        public async Task AceitarConsultaAsync_DeveAceitarConsulta_QuandoDadosSaoValidos()
        {
            // Arrange
            var consultaId = 1;
            var medicoId = 2;

            var consulta = new Consulta
            {
                Id = consultaId,
                MedicoId = medicoId,
                Status = StatusConsulta.Pendente,
                DataHora = DateTime.Now 
            };

            _consultaRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(consultaId))
                .ReturnsAsync(consulta);

            _consultaRepositoryMock
                .Setup(repo => repo.AtualizarStatusAsync(consultaId, StatusConsulta.Aceita,null))
                .ReturnsAsync(true);

            // Act
            var resultado = await _service.AceitarConsultaAsync(consultaId, medicoId);

            // Assert
            Assert.Equal("Consulta aceita com sucesso.", resultado);
        }

        [Fact]
        public async Task RecusarConsultaAsync_DeveRecusarConsulta_QuandoDadosSaoValidos()
        {
            // Arrange
            var consultaId = 1;
            var medicoId = 2;

            var consulta = new Consulta
            {
                Id = consultaId,
                MedicoId = medicoId,
                Status = StatusConsulta.Pendente,
                DataHora = DateTime.Now
            };

            _consultaRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(consultaId))
                .ReturnsAsync(consulta);

            _consultaRepositoryMock
                .Setup(repo => repo.AtualizarStatusAsync(consultaId, StatusConsulta.Recusada,null))
                .ReturnsAsync(true);

            // Act
            var resultado = await _service.RecusarConsultaAsync(consultaId, medicoId);

            // Assert
            Assert.Equal("Consulta recusada com sucesso.", resultado);
        }

        [Fact]
        public async Task ObterConsultasPorUsuarioAsync_DeveRetornarConsultasDoMedico_QuandoUsuarioEhMedico()
        {
            // Arrange
            var usuarioId = 1;
            var role = Role.Medico;

            var consultas = new List<Consulta>
            {
                new() { Id = 1, MedicoId = usuarioId, DataHora = DateTime.Now },
                new() { Id = 2, MedicoId = usuarioId, DataHora = DateTime.Now }
            };

            _consultaRepositoryMock
                .Setup(repo => repo.ObterConsultasDoMedicoAsync(usuarioId))
                .ReturnsAsync(consultas);

            // Act
            var resultado = await _service.ObterConsultasPorUsuarioAsync(usuarioId, role);

            // Assert
            Assert.Equal(2, resultado.Count());
        }
    }
}

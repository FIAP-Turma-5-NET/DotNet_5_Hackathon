using AutoMapper;

using FIAP_HealthMed.Application.Model.Consulta;
using FIAP_HealthMed.Application.Service;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Producer.Interface;
using Moq;

namespace FIAP.HealthMed.Tests.Unit
{
    public class ConsultaApplicationServiceTests
    {
        private readonly Mock<IConsultaDomainService> _consultaDomainServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IConsultaProducer> _consultaProducerMock;
        private readonly ConsultaApplicationService _service;

        public ConsultaApplicationServiceTests()
        {
            _consultaDomainServiceMock = new Mock<IConsultaDomainService>();
            _mapperMock = new Mock<IMapper>();
            _consultaProducerMock = new Mock<IConsultaProducer>();
            _service = new ConsultaApplicationService(_consultaDomainServiceMock.Object, _mapperMock.Object, _consultaProducerMock.Object);
        }

        [Fact]
        public async Task AgendarConsulta_DeveRetornarMensagemSucesso()
        {
            // Arrange
            var request = new ConsultaModelRequest
            {
                DataHora = DateTime.UtcNow.AddDays(1),
                EspecialidadeId = 1,
                MedicoId = 123,
                PacienteId = 456
            };
            var entity = new Consulta
            {
                DataHora = request.DataHora, 
                EspecialidadeId = request.EspecialidadeId,
                MedicoId = request.MedicoId,
                PacienteId = request.PacienteId
            };
            var expectedMessage = "Consulta enviada para processamento assíncrono com sucesso!";

            _mapperMock.Setup(m => m.Map<ConsultaMensagem>(request)).Returns(new ConsultaMensagem());
            _consultaProducerMock
                .Setup(p => p.EnviarConsultaAsync(It.IsAny<ConsultaMensagem>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.AgendarConsulta(request);

            // Assert
            Assert.Equal(expectedMessage, result);
            _consultaProducerMock.Verify(p => p.EnviarConsultaAsync(It.IsAny<ConsultaMensagem>()), Times.Once);
        }

        [Fact]
        public async Task AceitarConsulta_DeveRetornarMensagemSucesso()
        {
            // Arrange
            int consultaId = 1;
            int usuarioId = 1;
            var expectedMessage = "Consulta aceita com sucesso!";

            _consultaDomainServiceMock
                .Setup(s => s.AceitarConsultaAsync(consultaId, usuarioId))
                .ReturnsAsync(expectedMessage);

            // Act
            var result = await _service.AceitarConsulta(consultaId, usuarioId);

            // Assert
            Assert.Equal(expectedMessage, result);
            _consultaDomainServiceMock.Verify(s => s.AceitarConsultaAsync(consultaId, usuarioId), Times.Once);
        }

        [Fact]
        public async Task CancelarConsulta_DeveRetornarMensagemSucesso()
        {
            // Arrange
            int consultaId = 1;
            int usuarioId = 1;
            string justificativa = "Motivo do cancelamento";
            var expectedMessage = "Consulta cancelada com sucesso!";

            _consultaDomainServiceMock
                .Setup(s => s.CancelarConsultaAsync(consultaId, usuarioId, justificativa))
                .ReturnsAsync(expectedMessage);

            // Act
            var result = await _service.CancelarConsulta(consultaId, usuarioId, justificativa);

            // Assert
            Assert.Equal(expectedMessage, result);
            _consultaDomainServiceMock.Verify(s => s.CancelarConsultaAsync(consultaId, usuarioId, justificativa), Times.Once);
        }

        [Fact]
        public async Task ObterConsultas_DeveRetornarListaDeConsultas()
        {
            // Arrange
            int usuarioId = 1;
            var role = Role.Paciente;
            var consultasEntity = new List<Consulta>
            {
                new Consulta
                {
                    DataHora = DateTime.Now,
                    EspecialidadeId = 1,
                    MedicoId = 123,
                    PacienteId = 456
                },
                new Consulta
                {
                    DataHora = DateTime.Now,
                    EspecialidadeId = 2,
                    MedicoId = 789,
                    PacienteId = 101
                }
            };
            var consultasResponse = new List<ConsultaModelResponse>
            {
                new ConsultaModelResponse(),
                new ConsultaModelResponse()
            };

            _consultaDomainServiceMock
                .Setup(s => s.ObterConsultasPorUsuarioAsync(usuarioId, role))
                .ReturnsAsync(consultasEntity);
            _mapperMock
                .Setup(m => m.Map<IEnumerable<ConsultaModelResponse>>(consultasEntity))
                .Returns(consultasResponse);

            // Act
            var result = await _service.ObterConsultas(usuarioId, role);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(consultasResponse.Count, result.Count());
            _consultaDomainServiceMock.Verify(s => s.ObterConsultasPorUsuarioAsync(usuarioId, role), Times.Once);
        }

        [Fact]
        public async Task RecusarConsulta_DeveRetornarMensagemSucesso()
        {
            // Arrange
            int consultaId = 1;
            int usuarioId = 1;
            var expectedMessage = "Consulta recusada com sucesso!";

            _consultaDomainServiceMock
                .Setup(s => s.RecusarConsultaAsync(consultaId, usuarioId))
                .ReturnsAsync(expectedMessage);

            // Act
            var result = await _service.RecusarConsulta(consultaId, usuarioId);

            // Assert
            Assert.Equal(expectedMessage, result);
            _consultaDomainServiceMock.Verify(s => s.RecusarConsultaAsync(consultaId, usuarioId), Times.Once);
        }

        [Fact]
        public async Task ObterConsultas_DeveRetornarListaVazia_QuandoNaoHouverConsultas()
        {
            // Arrange
            int usuarioId = 1;
            var role = Role.Paciente;
            var consultasEntity = new List<Consulta>();
            var consultasResponse = new List<ConsultaModelResponse>();

            _consultaDomainServiceMock
                .Setup(s => s.ObterConsultasPorUsuarioAsync(usuarioId, role))
                .ReturnsAsync(consultasEntity);
            _mapperMock
                .Setup(m => m.Map<IEnumerable<ConsultaModelResponse>>(consultasEntity))
                .Returns(consultasResponse);

            // Act
            var result = await _service.ObterConsultas(usuarioId, role);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}

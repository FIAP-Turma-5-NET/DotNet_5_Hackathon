using FIAP_HealthMed.Application.Model.Consulta;
using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Application.Interface
{
    public interface IConsultaApplicationService
    {
        Task<string> AgendarConsulta(ConsultaModelRequest request);
        Task<string> CancelarConsulta(int consultaId, int usuarioId, string justificativa);
        Task<string> AceitarConsulta(int consultaId, int usuarioId);
        Task<string> RecusarConsulta(int consultaId, int usuarioId);
        Task<IEnumerable<ConsultaModelResponse>> ObterConsultas(int usuarioId, Role role);

    }
}

using FIAP_HealthMed.Application.Model.Consulta;
using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Application.Interface
{
    public interface IConsultaApplicationService
    {
        Task<string> AgendarConsulta(ConsultaModelRequest request);
        Task<string> CancelarConsulta(int consultaId, string justificativa);
        Task<string> AceitarConsulta(int consultaId);
        Task<string> RecusarConsulta(int consultaId);
        Task<IEnumerable<ConsultaModelResponse>> ObterConsultas(int usuarioId, Role role);

    }
}

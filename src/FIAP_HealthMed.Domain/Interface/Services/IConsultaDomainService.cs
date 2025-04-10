using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IConsultaDomainService
    {
        Task<string> AgendarConsultaAsync(Consulta consulta);
        Task<string> CancelarConsultaAsync(int consultaId, string justificativa);
        Task<string> AceitarConsultaAsync(int consultaId);
        Task<string> RecusarConsultaAsync(int consultaId);
        Task<IEnumerable<Consulta>> ObterConsultasPorUsuarioAsync(int usuarioId, Role role);
    }
}

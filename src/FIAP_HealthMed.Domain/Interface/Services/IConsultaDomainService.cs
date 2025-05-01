using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IConsultaDomainService
    {
        Task<string> AgendarConsultaAsync(Consulta consulta);
        Task<string> CancelarConsultaAsync(int consultaId,int usuarioConsulta, string justificativa);
        Task<string> AceitarConsultaAsync(int consultaId, int usuarioId);
        Task<string> RecusarConsultaAsync(int consultaId, int usuarioId);
        Task<IEnumerable<Consulta>> ObterConsultasPorUsuarioAsync(int usuarioId, Role role);
    }
}

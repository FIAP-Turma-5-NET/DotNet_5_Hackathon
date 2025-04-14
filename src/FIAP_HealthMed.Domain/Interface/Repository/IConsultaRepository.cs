using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IConsultaRepository
    {
        Task<int> AgendarAsync(Consulta consulta);
        Task<Consulta?> ObterPorIdAsync(int id);
        Task<IEnumerable<Consulta>> ObterPorUsuarioIdAsync(int usuarioId, Role role);
        Task<bool> AtualizarStatusAsync(int consultaId, StatusConsulta status, string? justificativa = null);
    }
}

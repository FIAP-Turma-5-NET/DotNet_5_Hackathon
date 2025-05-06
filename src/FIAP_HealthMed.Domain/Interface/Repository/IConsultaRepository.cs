using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IConsultaRepository
    {
        Task<int> AgendarAsync(Consulta consulta);
        Task<Consulta?> ObterPorIdAsync(int id);
        Task<IEnumerable<Consulta>> ObterConsultasDoMedicoAsync(int medicoId);
        Task<IEnumerable<Consulta>> ObterConsultasDoPacienteAsync(int pacienteId);
        Task<bool> AtualizarStatusAsync(int consultaId, StatusConsulta status, string? justificativa = null);
    }
}

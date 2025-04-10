using FIAP_HealthMed.Domain.Entity;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IHorarioDisponivelDomainService
    {
        Task CadastrarHorariosAsync(int medicoId, IEnumerable<DateTime> horarios);
        Task<IEnumerable<HorarioDisponivel>> ObterPorMedicoIdAsync(int medicoId);
        Task EditarHorarioAsync(int horarioId, DateTime novoHorario);
        Task RemoverHorarioAsync(int horarioId);
    }
}

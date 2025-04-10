using FIAP_HealthMed.Domain.Entity;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IHorarioDisponivelRepository
    {
        Task<IEnumerable<HorarioDisponivel>> ObterPorMedicoIdAsync(int medicoId);
        Task<bool> InserirHorariosAsync(IEnumerable<HorarioDisponivel> horarios);
        Task<bool> AtualizarHorarioAsync(int horarioId, DateTime novoHorario);
        Task<bool> RemoverHorarioAsync(int horarioId);
    }
}

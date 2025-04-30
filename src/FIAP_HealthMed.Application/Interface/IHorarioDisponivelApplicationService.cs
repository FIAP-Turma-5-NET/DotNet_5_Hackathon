using FIAP_HealthMed.Application.Model.HorarioDisponivel;

namespace FIAP_HealthMed.Application.Interface
{
    public interface IHorarioDisponivelApplicationService
    {
        Task CadastrarHorarios(int medicoId, IEnumerable<DateTime> horarios);
        Task<IEnumerable<HorarioDisponivelModelRequest>> ObterHorarios(int medicoId);
        Task EditarHorario(int horarioId, DateTime novoHorario);
        Task RemoverHorario(int horarioId);
    }
}

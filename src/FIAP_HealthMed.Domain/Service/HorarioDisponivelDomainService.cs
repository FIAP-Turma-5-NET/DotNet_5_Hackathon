using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Domain.Service
{
    public class HorarioDisponivelDomainService : IHorarioDisponivelDomainService
    {
        private readonly IHorarioDisponivelRepository _horarioRepository;

        public HorarioDisponivelDomainService(IHorarioDisponivelRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public async Task CadastrarHorariosAsync(int medicoId, IEnumerable<DateTime> horarios)
        {
            var lista = horarios.Select(h => new HorarioDisponivel
            {
                DataHora = h,
                Ocupado = false,
                MedicoId = medicoId
            });

            var result = await _horarioRepository.InserirHorariosAsync(lista);

            if (!result)
                throw new InvalidOperationException("Erro ao cadastrar horários.");
        }

        public async Task<IEnumerable<HorarioDisponivel>> ObterPorMedicoIdAsync(int medicoId)
        {
            return await _horarioRepository.ObterPorMedicoIdAsync(medicoId);
        }

        public async Task EditarHorarioAsync(int horarioId, DateTime novoHorario)
        {
            var result = await _horarioRepository.AtualizarHorarioAsync(horarioId, novoHorario);

            if (!result)
                throw new InvalidOperationException("Erro ao editar horário.");
        }

        public async Task RemoverHorarioAsync(int horarioId)
        {
            var result = await _horarioRepository.RemoverHorarioAsync(horarioId);

            if (!result)
                throw new InvalidOperationException("Erro ao remover horário.");
        }
    }
}

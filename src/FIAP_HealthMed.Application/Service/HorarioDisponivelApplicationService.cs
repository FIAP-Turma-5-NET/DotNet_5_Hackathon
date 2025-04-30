using AutoMapper;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Model.HorarioDisponivel;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Application.Service
{
    public class HorarioDisponivelApplicationService : IHorarioDisponivelApplicationService
    {
        private readonly IHorarioDisponivelDomainService _horarioDisponivelDomainService;
        private readonly IMapper _mapper;

        public HorarioDisponivelApplicationService(IHorarioDisponivelDomainService horarioDisponivelDomainService, IMapper mapper)
        {
            _horarioDisponivelDomainService = horarioDisponivelDomainService;
            _mapper = mapper;
        }
        public async Task CadastrarHorarios(int medicoId, IEnumerable<DateTime> horarios)
        {
             await _horarioDisponivelDomainService.CadastrarHorariosAsync(medicoId, horarios);
        }

        public async Task EditarHorario(int horarioId, DateTime novoHorario)
        {
            await _horarioDisponivelDomainService.EditarHorarioAsync(horarioId, novoHorario);
        }

        public async Task<IEnumerable<HorarioDisponivelModelRequest>> ObterHorarios(int medicoId)
        {
            var entities = await _horarioDisponivelDomainService.ObterPorMedicoIdAsync(medicoId);

            return _mapper.Map<IEnumerable<HorarioDisponivelModelRequest>>(entities);
        }

        public async Task RemoverHorario(int horarioId)
        {
            await _horarioDisponivelDomainService.RemoverHorarioAsync(horarioId);
        }
    }
}

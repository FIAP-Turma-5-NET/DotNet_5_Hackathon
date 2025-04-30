using AutoMapper;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Model.Especialidade;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Services;

namespace FIAP_HealthMed.Application.Service
{
    public class EspecialidadeApplicationService : IEspecialidadeApplicationService
    {
        private readonly IEspecialidadeDomainService _especialidadeDomainService;
        private readonly IMapper _mapper;

        public EspecialidadeApplicationService(IEspecialidadeDomainService especialidadeDomainService, IMapper mapper)
        {
            _especialidadeDomainService = especialidadeDomainService;
            _mapper = mapper;
        }
        public async Task<string> CadastrarEspecialidade(EspecialidadeModelRequest request)
        {
            var entity = _mapper.Map<Especialidade>(request);
            return await _especialidadeDomainService.CadastrarAsync(entity);
        }

        public async Task<EspecialidadeModelResponse?> ObterPorId(int id)
        {
            var entity = await _especialidadeDomainService.ObterPorIdAsync(id);
            return entity == null ? null : _mapper.Map<EspecialidadeModelResponse>(entity);
        }

        public async Task<IEnumerable<EspecialidadeModelResponse>> ObterTodas()
        {
            var entities = await _especialidadeDomainService.ObterTodasAsync();
            return _mapper.Map<IEnumerable<EspecialidadeModelResponse>>(entities);
        }
    }
}

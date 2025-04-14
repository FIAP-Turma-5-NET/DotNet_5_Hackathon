using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Interface.Services;

namespace FIAP_HealthMed.Domain.Service
{
    public class EspecialidadeDomainService : IEspecialidadeDomainService
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public EspecialidadeDomainService(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }

        public async Task<string> CadastrarAsync(Especialidade especialidade)
        {
            var id = await _especialidadeRepository.CadastrarAsync(especialidade);
            return id > 0 ? "Especialidade cadastrada com sucesso." : throw new InvalidOperationException("Erro ao cadastrar especialidade.");
        }

        public async Task<IEnumerable<Especialidade>> ObterTodasAsync()
        {
            return await _especialidadeRepository.ObterTodasAsync();
        }

        public async Task<Especialidade?> ObterPorIdAsync(int id)
        {
            return await _especialidadeRepository.ObterPorIdAsync(id);
        }
    }
}

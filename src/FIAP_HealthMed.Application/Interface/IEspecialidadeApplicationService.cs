using FIAP_HealthMed.Application.Model.Especialidade;

namespace FIAP_HealthMed.Application.Interface
{
    public interface IEspecialidadeApplicationService
    {
        Task<string> CadastrarEspecialidade(EspecialidadeModelRequest request);
        Task<IEnumerable<EspecialidadeModelResponse>> ObterTodas();
        Task<EspecialidadeModelResponse?> ObterPorId(int id);
    }
}

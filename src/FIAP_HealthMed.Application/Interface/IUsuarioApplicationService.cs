using FIAP_HealthMed.Application.Model.Usuario;

namespace FIAP_HealthMed.Application.Interface
{
    public interface IUsuarioApplicationService
    {
        Task<string> CadastrarAsync(UsuarioModelRequest request);
        Task<IEnumerable<UsuarioModelResponse>> ListarMedicos(string? especialidade);
        Task<UsuarioModelResponse> ObterPorId(int id);
    }
}

using FIAP_HealthMed.Application.Model.Auth;
using FIAP_HealthMed.Application.Model.Usuario;

namespace FIAP_HealthMed.Application.Interface
{
    public interface IUsuarioApplicationService
    {
        Task<string> CadastrarAsync(UsuarioModelRequest request);
        Task<IEnumerable<UsuarioModelResponse>> ListarMedicos(BuscaMedicoModelRequest request);
        Task<UsuarioModelResponse> ObterPorId(int id);
        Task<string> InserirEspecialidadesUsuarioAsync(int usuarioId, IEnumerable<int> especialidadeIds);
    }
}

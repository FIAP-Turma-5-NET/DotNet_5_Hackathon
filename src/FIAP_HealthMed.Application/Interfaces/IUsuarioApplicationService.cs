using FIAP_HealthMed.Application.Model.Auth;
using FIAP_HealthMed.Application.Model.Usuario;

namespace FIAP_HealthMed.Application.Interface
{
    public interface IUsuarioApplicationService
    {
        Task<string> CadastrarAsync(UsuarioModelRequest request);
        Task<IEnumerable<UsuarioModelResponse>> ListarMedicos(int? especialidadeId = null);
        Task<UsuarioModelResponse> ObterPorId(int id);
        Task<bool> EfetuarLoginAsync(AuthLoginModelRequest request);
        Task<string> InserirEspecialidadesUsuarioAsync(int usuarioId, IEnumerable<int> especialidadeIds);
    }
}

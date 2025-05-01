using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Domain.Service
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> CadastrarAsync(Usuario usuario)
        {
            usuario.TratarTelefone(usuario.Telefone);

            var id = await _usuarioRepository.CadastrarAsync(usuario);

            return id > 0;
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            return await _usuarioRepository.ObterPorIdAsync(id);
        }

        public async Task<bool> VerificarExistentePorCpfOuEmailAsync(string cpf, string email)
        {
            return await _usuarioRepository.VerificarExistentePorCpfOuEmailAsync(cpf, email);
        }

        public async Task<IEnumerable<Usuario>> ListarMedicosAsync(int? especialidadeId = null)
        {
            return await _usuarioRepository.ListarMedicosAsync(especialidadeId);
        }


    }
}

using FIAP_HealthMed.Domain.Entity;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IUsuarioDomainService
    {
        Task<Usuario?> ObterPorIdAsync(int id);
        Task<bool> VerificarExistentePorCpfOuEmailAsync(string cpf, string email);
        Task<IEnumerable<Usuario>> ListarMedicosAsync(string? especialidade = null);
    }
}

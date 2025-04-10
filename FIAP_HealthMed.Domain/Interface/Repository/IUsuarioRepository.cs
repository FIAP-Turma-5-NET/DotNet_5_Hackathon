using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAP_HealthMed.Domain.Entity;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorIdAsync(int id);
        Task<Usuario?> ObterPorCpfOuEmailAsync(string login);
        Task<bool> VerificarExistentePorCpfOuEmailAsync(string cpf, string email);
        Task<IEnumerable<Usuario>> ListarMedicosAsync(string? especialidade = null);
        Task<int> CadastrarAsync(Usuario usuario);
    }
}

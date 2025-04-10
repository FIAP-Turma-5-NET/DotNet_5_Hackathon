using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAP_HealthMed.Domain.Entity;

namespace FIAP_HealthMed.Domain.Interface.Repository
{
    public interface IEspecialidadeRepository
    {
        Task<int> CadastrarAsync(Especialidade especialidade);
        Task<IEnumerable<Especialidade>> ObterTodasAsync();
        Task<Especialidade?> ObterPorIdAsync(int id);
    }
}

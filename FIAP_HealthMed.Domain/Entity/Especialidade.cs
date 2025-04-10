using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_HealthMed.Domain.Entity
{
    public class Especialidade : EntityBase
    {
        public required string Nome { get; set; }
        public ICollection<Usuario> Medicos { get; set; } = new List<Usuario>();
    }
}

using System.Collections.Generic;
using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Domain.Entity
{
    public class Usuario : EntityBase
    {
        public required string Nome { get; set; }    
        public required string CPF { get; set; }
        public required string DDD { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }
        public required string SenhaHash { get; set; }

        public required Role Role { get; set; }

        public bool Ativo { get; set; }

        public string? CRM { get; set; }
        public ICollection<Especialidade>? Especialidades { get; set; }

        public void TratarTelefone(string telefone)
        {
            var telefoneSemMascara = telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            DDD = telefoneSemMascara.Substring(0, 2);
            Telefone = telefoneSemMascara.Substring(2);
        }
       

    }
}

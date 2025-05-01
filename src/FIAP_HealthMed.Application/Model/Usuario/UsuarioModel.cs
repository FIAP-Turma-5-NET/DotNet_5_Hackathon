using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Application.Model.Usuario
{
    public record UsuarioModel
    {
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string DDD { get; set; }
        public required string Telefone { get; set; }
        public required string Email { get; set; }
        public required string SenhaHash { get; set; }

        public required Role Role { get; set; }

        public string? CRM { get; set; }
        public int? EspecialidadeId { get; set; }
    }
}

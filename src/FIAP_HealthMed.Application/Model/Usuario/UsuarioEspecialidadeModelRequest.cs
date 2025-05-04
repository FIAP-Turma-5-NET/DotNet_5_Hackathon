using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_HealthMed.Application.Model.Usuario
{
    public record UsuarioEspecialidadeModelRequest
    {
        public List<int> EspecialidadeIds { get; init; } = new();
    }
}

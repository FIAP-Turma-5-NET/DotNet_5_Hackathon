using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Model
{
    public class ConsultaMensagem
    {
        public required DateTime DataHora { get; set; }
        public required int MedicoId { get; set; }
        public required int PacienteId { get; set; }
        public required int EspecialidadeId { get; set; }
        public required string TipoEvento { get; set; }
    }
}

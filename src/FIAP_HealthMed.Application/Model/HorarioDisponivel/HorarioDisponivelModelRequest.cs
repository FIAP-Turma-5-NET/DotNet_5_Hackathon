using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_HealthMed.Application.Model.HorarioDisponivel
{
    public record HorarioDisponivelModelRequest
    {
        public required DateTime DataHora { get; set; }
        public int MedicoId { get; set; }
    }
}

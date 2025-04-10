using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Domain.Entity
{
    public class Consulta : EntityBase
    {
        public required DateTime DataHora { get; set; }
        public decimal Valor { get; set; }

        public StatusConsulta Status { get; set; } = StatusConsulta.Pendente;
        public string? JustificativaCancelamento { get; set; }

        public int MedicoId { get; set; }
        public Usuario Medico { get; set; }

        public int PacienteId { get; set; }
        public Usuario Paciente { get; set; }
    }
}

using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Application.Model.Consulta
{
    public class ConsultaModelResponse
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public decimal Valor { get; set; }
        public StatusConsulta Status { get; set; }
        public string? JustificativaCancelamento { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
    }
}

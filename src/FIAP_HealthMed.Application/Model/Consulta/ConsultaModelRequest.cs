namespace FIAP_HealthMed.Application.Model.Consulta
{
    public class ConsultaModelRequest
    {
        public required DateTime DataHora { get; set; }
        public decimal Valor { get; set; }

        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
    }
}

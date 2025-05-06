namespace FIAP_HealthMed.Application.Model.Consulta
{
    public record ConsultaModelRequest
    {
        public required DateTime DataHora { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public required int EspecialidadeId { get; init; }
    }
}

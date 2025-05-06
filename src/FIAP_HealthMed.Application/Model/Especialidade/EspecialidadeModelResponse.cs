namespace FIAP_HealthMed.Application.Model.Especialidade
{
    public record EspecialidadeModelResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public decimal ValorConsulta { get; set; }
    }
}

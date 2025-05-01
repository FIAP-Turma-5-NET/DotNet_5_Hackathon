namespace FIAP_HealthMed.Application.Model.HorarioDisponivel
{
    public record HorarioDisponivelModelResponse
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public bool Ocupado { get; set; }
    }
}

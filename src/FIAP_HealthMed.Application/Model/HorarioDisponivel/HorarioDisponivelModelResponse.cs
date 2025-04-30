namespace FIAP_HealthMed.Application.Model.HorarioDisponivel
{
    internal class HorarioDisponivelModelResponse
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public bool Ocupado { get; set; }
    }
}

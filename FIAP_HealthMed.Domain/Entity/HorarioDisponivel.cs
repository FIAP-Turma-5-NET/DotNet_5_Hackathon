namespace FIAP_HealthMed.Domain.Entity
{
    public class HorarioDisponivel : EntityBase
    {
        public required DateTime DataHora { get; set; }
        public bool Ocupado { get; set; }
        public int MedicoId { get; set; }
        public Usuario Medico { get; set; }
    }
}

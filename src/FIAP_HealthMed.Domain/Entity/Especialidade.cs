namespace FIAP_HealthMed.Domain.Entity
{
    public class Especialidade : EntityBase
    {
        public required string Nome { get; set; }
        public ICollection<Usuario> Medicos { get; set; } = new List<Usuario>();
    }
}

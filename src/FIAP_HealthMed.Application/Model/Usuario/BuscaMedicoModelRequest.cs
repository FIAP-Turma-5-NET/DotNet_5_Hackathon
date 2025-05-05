namespace FIAP_HealthMed.Application.Model.Usuario
{
    public record BuscaMedicoModelRequest
    {
        public int? EspecialidadeId { get; set; }
        public string? Nome { get; set; }
        public string? CRM { get; set; }
    }
} 
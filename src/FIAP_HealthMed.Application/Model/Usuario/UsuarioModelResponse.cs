using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Application.Model.Usuario
{
    public record UsuarioModelResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string DDD { get; set; }
        public string Telefone { get; set; }
        public Role Role { get; set; }
        public bool Ativo { get; set; }

        public string? CRM { get; set; }
        public List<string?> Especialidades { get; set; } = new();
    }
}

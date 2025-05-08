namespace Shared.Model
{
    public class UsuarioMensagem
    {
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; } 
        public required string DDD { get; set; }
        public required string Telefone { get; set; }
        public required string TipoUsuario { get; set; } 
        public bool Ativo { get; set; }

        public string? CRM { get; set; }
        public List<int?> EspecialidadeIds { get; set; } = new();
        public required string TipoEvento { get; set; }
    }
}




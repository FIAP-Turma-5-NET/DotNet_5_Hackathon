namespace FIAP_HealthMed.Application.Model.Usuario
{
    public class UsuarioLoginModelRequest
    {
        public required string Login { get; set; } 
        public required string Senha { get; set; }
    }
}

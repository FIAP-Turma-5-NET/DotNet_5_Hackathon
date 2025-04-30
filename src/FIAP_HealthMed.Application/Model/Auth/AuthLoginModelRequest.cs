namespace FIAP_HealthMed.Application.Model.Auth
{
    public class AuthLoginModelRequest
    {
        public required string Login { get; set; }
        public required string Senha { get; set; }
    }
}

using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Application.Model.Auth
{
    public class AuthLoginModelResponse
    {
        public required string Token { get; set; }
        public required string Nome { get; set; }
        public Role Role { get; set; }
    }
}

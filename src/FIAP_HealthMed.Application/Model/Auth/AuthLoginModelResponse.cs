using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Application.Model.Auth
{
    public class AuthLoginModelResponse
    {
        public string Token { get; set; }
        public string Nome { get; set; }
        public Role Role { get; set; }
    }
}

using FIAP_HealthMed.Application.Model.Auth;

namespace FIAP_HealthMed.Application.Interfaces
{
    public interface IAuthApplicationService
    {
        Task<string> AutenticarLoginAsync(AuthLoginModelRequest request);
    }
}

using FIAP_HealthMed.Application.Enums;
using FIAP_HealthMed.Domain.Entity;

namespace FIAP_HealthMed.Application.Interface
{
    public interface ILoginStrategy
    {
        TipoLogin TipoLogin { get; }
        Task<Usuario?> ObterUsuarioAsync(string login);
    }

}

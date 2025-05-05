using FIAP_HealthMed.Application.Enums;
using FIAP_HealthMed.Application.Interface;


namespace FIAP_HealthMed.Application.Interfaces
{
    public interface ILoginStrategyResolver
    {
        ILoginStrategy Resolver(TipoLogin tipoLogin);
    }
}

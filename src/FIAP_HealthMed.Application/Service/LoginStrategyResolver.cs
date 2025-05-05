using FIAP_HealthMed.Application.Enums;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Interfaces;


namespace FIAP_HealthMed.Application.Service
{
    public class LoginStrategyResolver : ILoginStrategyResolver
    {
        private readonly Dictionary<TipoLogin, ILoginStrategy> _estrategias;

        public LoginStrategyResolver(IEnumerable<ILoginStrategy> estrategias)
        {
            _estrategias = estrategias.ToDictionary(e => e.TipoLogin);
        }

        public ILoginStrategy Resolver(TipoLogin tipoLogin)
        {
            if (!_estrategias.TryGetValue(tipoLogin, out var strategy))
                throw new ArgumentException($"Tipo de login não suportado: {tipoLogin}");

            return strategy;
        }
    }
}

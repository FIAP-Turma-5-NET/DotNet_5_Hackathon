using FIAP_HealthMed.Application.Enums;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Application.Strategies
{
    public class LoginEmailStrategy : ILoginStrategy
    {
        private readonly IUsuarioRepository _repository;

        public LoginEmailStrategy(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public TipoLogin TipoLogin => TipoLogin.Email;      

        public Task<Usuario?> ObterUsuarioAsync(string login)
        {
            return _repository.ObterPorEmailAsync(login);
        }
    }
}

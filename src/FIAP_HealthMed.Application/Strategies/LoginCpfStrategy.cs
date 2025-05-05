using FIAP_HealthMed.Application.Enums;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Application.Strategies
{
    public class LoginCpfStrategy : ILoginStrategy
    {
        private readonly IUsuarioRepository _repository;

        public LoginCpfStrategy(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public TipoLogin TipoLogin => TipoLogin.CPF;

        public Task<Usuario?> ObterUsuarioAsync(string login)
        {
            return _repository.ObterPorCpfAsync(login);
        }
    }
}

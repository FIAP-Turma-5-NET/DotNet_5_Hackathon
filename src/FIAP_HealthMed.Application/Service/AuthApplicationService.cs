using FIAP_HealthMed.Application.Enums;
using FIAP_HealthMed.Application.Interfaces;
using FIAP_HealthMed.Application.Model.Auth;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Application.Service
{
    public class AuthApplicationService : IAuthApplicationService
    {
        private readonly ILoginStrategyResolver _loginStrategyResolver;
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly ITokenService _tokenService;

        public AuthApplicationService(
            ILoginStrategyResolver loginStrategyResolver,
            IUsuarioDomainService usuarioDomainService,
            ITokenService tokenService)
        {
            _loginStrategyResolver = loginStrategyResolver;
            _usuarioDomainService = usuarioDomainService;
            _tokenService = tokenService;
        }

        public async Task<string> AutenticarLoginAsync(AuthLoginModelRequest request)
        {
            var tipoLogin = ObterTipoLogin(request.Login);
            var strategy = _loginStrategyResolver.Resolver(tipoLogin);
            var usuario = await strategy.ObterUsuarioAsync(request.Login);

            if (usuario == null)
                throw new UnauthorizedAccessException("Login ou senha inválidos. Verifique suas credenciais e tente novamente.");

            var loginValido = _usuarioDomainService.VerificarLogin(usuario, request.Senha);

            if (!loginValido)
                throw new UnauthorizedAccessException("Login ou senha inválidos. Verifique suas credenciais e tente novamente.");

            var token = _tokenService.GerarToken(usuario);

            return token;
        }

        private static TipoLogin ObterTipoLogin(string login)
        {
            if (LoginEmail(login))
                return TipoLogin.Email;
            else if (LoginCpf(login))
                return TipoLogin.CPF;
            else
                return TipoLogin.CRM;
        }

        private static bool LoginEmail(string email)
        {
            try
            {
                var objEmail = new System.Net.Mail.MailAddress(email);
                return objEmail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool LoginCpf(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());
            return cpf.Length == 11;
        }
    }

}

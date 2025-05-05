using AutoMapper;

using FIAP_HealthMed.Application.Enums;
using FIAP_HealthMed.Domain.Helpers;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Interfaces;
using FIAP_HealthMed.Application.Model.Auth;
using FIAP_HealthMed.Application.Model.Usuario;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Interface.Services;


namespace FIAP_HealthMed.Application.Service
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {

        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IEspecialidadeDomainService _especialidadeDomainService;
        private readonly IMapper _mapper;
        private readonly ILoginStrategyResolver _loginStrategyResolver;

        public UsuarioApplicationService(IUsuarioDomainService usuarioDomainService, 
            IEspecialidadeDomainService especialidadeDomainService, 
            IMapper mapper,
            ILoginStrategyResolver loginStrategyResolver)
        {
            _usuarioDomainService = usuarioDomainService;
            _especialidadeDomainService = especialidadeDomainService;
            _mapper = mapper;
            _loginStrategyResolver = loginStrategyResolver;
        } 

        public async Task<string> CadastrarAsync(UsuarioModelRequest request)
        {
            var existe = await _usuarioDomainService.VerificarExistentePorCpfOuEmailAsync(request.CPF, request.Email);
            if (existe)
                throw new InvalidOperationException("Usuário já cadastrado com este CPF ou Email.");

            var usuario = _mapper.Map<Usuario>(request);
            usuario.TratarTelefone(request.Telefone);
            usuario.SenhaHash = PasswordHasher.HashPassword(request.Senha);

            var usuarioId = await _usuarioDomainService.CadastrarAsync(usuario);

            if (request.Role == Role.Medico && request.EspecialidadeIds != null)
            {
                
                var especialidadeIds = request.EspecialidadeIds
                    .Where(id => id.HasValue)   
                    .Select(id => id!.Value)    
                    .Distinct()                 
                    .ToList();

                if (especialidadeIds.Any())
                    await _usuarioDomainService.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds);
            }

            return "Usuário cadastrado com sucesso!";
        }

        public async Task<string> InserirEspecialidadesUsuarioAsync(int usuarioId, IEnumerable<int> especialidadeIds)
        {
             await _usuarioDomainService.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds);

            return "Especialidade vinculada com sucesso!";
        }

        public async Task<bool> EfetuarLoginAsync(AuthLoginModelRequest request)
        {
            var tipoLogin = ObterTipoLogin(request.Login);

            var strategy = _loginStrategyResolver.Resolver(tipoLogin);

            var usuario = await strategy.ObterUsuarioAsync(request.Login);

            if (usuario == null) 
                throw new InvalidOperationException("Usuário não encontrado!");

            return _usuarioDomainService.VerificarLogin(usuario, request.Senha);
        }

        public async Task<IEnumerable<UsuarioModelResponse>> ListarMedicos(int? especialidadeId = null)
        {
            var result = await _usuarioDomainService.ListarMedicosAsync(especialidadeId);

            return _mapper.Map<IEnumerable<UsuarioModelResponse>>(result);
        }

        public async Task<UsuarioModelResponse> ObterPorId(int id)
        {
            var user = await _usuarioDomainService.ObterPorIdAsync(id);

            return _mapper.Map<UsuarioModelResponse>(user);
        }

        public async Task<IEnumerable<UsuarioModelResponse>> BuscarMedicos(BuscaMedicoModelRequest request)
        {
            var result = await _usuarioDomainService.ListarMedicosAsync(
                request.EspecialidadeId,
                request.Nome,
                request.CRM
            );

            return _mapper.Map<IEnumerable<UsuarioModelResponse>>(result);
        }

        private TipoLogin ObterTipoLogin(string login)
        {
            if (LoginEmail(login))
                return TipoLogin.Email;
            else if (LoginCpf(login))
                return TipoLogin.CPF;
            else
                return TipoLogin.CRM;
        }

        private bool LoginEmail(string email)
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

        private bool LoginCpf(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());
            return cpf.Length == 11;
        }
    }
}

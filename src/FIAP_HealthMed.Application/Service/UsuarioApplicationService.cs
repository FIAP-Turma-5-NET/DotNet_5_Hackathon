using AutoMapper;
using FIAP_HealthMed.Domain.Helpers;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Model.Usuario;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Interface.Services;
using FIAP_HealthMed.Application.Enums;
using Shared.Model;
using FIAP_HealthMed.Producer.Interface;

namespace FIAP_HealthMed.Application.Service
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {

        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IEspecialidadeDomainService _especialidadeDomainService;
        private readonly IUsuarioProducer _usuarioProducer;
        private readonly IMapper _mapper;    

        public UsuarioApplicationService(IUsuarioDomainService usuarioDomainService, 
            IEspecialidadeDomainService especialidadeDomainService,
            IUsuarioProducer usuarioProducer,
            IMapper mapper        
            )
        {
            _usuarioDomainService = usuarioDomainService;
            _especialidadeDomainService = especialidadeDomainService;
            _usuarioProducer = usuarioProducer;
            _mapper = mapper;           
        } 

      public async Task<string> CadastrarAsync(UsuarioModelRequest request)
        {
            var usuarioExiste = await _usuarioDomainService.VerificarExistentePorCpfOuEmailAsync(request.CPF, request.Email);
            if (usuarioExiste)
            {
                throw new Exception("Usuário já cadastrado com este CPF ou email.");
            }
                    
            var usuarioMensagem = _mapper.Map<UsuarioMensagem>(request);
            usuarioMensagem.SenhaHash = PasswordHasher.HashPassword(request.Senha);
            usuarioMensagem.TipoEvento = "Cadastrar";
          
            await _usuarioProducer.EnviarUsuarioAsync(usuarioMensagem);

            return "Usuário enviado para processamento assíncrono com sucesso!";
        }

        public async Task<string> InserirEspecialidadesUsuarioAsync(int usuarioId, IEnumerable<int> especialidadeIds)
        {
             await _usuarioDomainService.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds);

            return "Especialidade vinculada com sucesso!";
        }

        public async Task<UsuarioModelResponse> ObterPorId(int id)
        {
            var user = await _usuarioDomainService.ObterPorIdAsync(id);

            return _mapper.Map<UsuarioModelResponse>(user);
        }

        public async Task<IEnumerable<UsuarioModelResponse>> ListarMedicos(BuscaMedicoModelRequest request)
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

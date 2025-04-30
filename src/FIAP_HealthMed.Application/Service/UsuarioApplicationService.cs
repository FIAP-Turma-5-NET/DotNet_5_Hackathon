using AutoMapper;
using FIAP_HealthMed.Application.Helpers;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Model.Usuario;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;


namespace FIAP_HealthMed.Application.Service
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IMapper _mapper;

        public UsuarioApplicationService(IUsuarioDomainService usuarioDomainService, IMapper mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _mapper = mapper;
        }

        public async Task<string> CadastrarAsync(UsuarioModelRequest request)
        {
            var existe = await _usuarioDomainService.VerificarExistentePorCpfOuEmailAsync(request.CPF, request.Email);

            if (existe)
            {
                throw new InvalidOperationException("Usuário já existente com esse CPF ou Email");
            }

            var usuario = _mapper.Map<Usuario>(request);
            usuario.SenhaHash = PasswordHasher.HashPassword(request.Senha);
            usuario.TratarTelefone(request.Telefone);

            var sucesso = await _usuarioDomainService.CadastrarAsync(usuario);

            return sucesso ? "Usuário cadastrado com sucesso." : throw new InvalidOperationException("Erro ao cadastrar usuário");

        }

        public async Task<IEnumerable<UsuarioModelResponse>> ListarMedicos(string? especialidade)
        {
            var result = await _usuarioDomainService.ListarMedicosAsync(especialidade);

            return _mapper.Map<IEnumerable<UsuarioModelResponse>>(result);
        }

        public async Task<UsuarioModelResponse> ObterPorId(int id)
        {
            var user = await _usuarioDomainService.ObterPorIdAsync(id);

            return _mapper.Map<UsuarioModelResponse>(user);
        }
    }
}

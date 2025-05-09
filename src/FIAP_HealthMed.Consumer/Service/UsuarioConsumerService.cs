using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FIAP_HealthMed.Consumer.Interface;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Service;
using Shared.Model;

namespace FIAP_HealthMed.Consumer.Service
{
    public class UsuarioConsumerService : IUsuarioConsumerService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private IMapper _mapper;

        public UsuarioConsumerService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<string> CadastrarUsuario(UsuarioMensagem request)
        {

            var response = string.Empty;

            var existe = await _usuarioRepository.VerificarExistentePorCpfOuEmailAsync(request.CPF, request.Email);
            if (existe)
                throw new InvalidOperationException("Usuário já cadastrado com este CPF ou Email.");

            if (!Enum.TryParse<Role>(request.TipoUsuario, true, out var tipoUsuarioEnum))
                throw new InvalidOperationException("Tipo de usuário inválido. Use 'Medico' ou 'Paciente'.");

            var usuario = _mapper.Map<Usuario>(request);
            usuario.Role = tipoUsuarioEnum;
            usuario.TratarTelefone(request.Telefone);
            
            var usuarioId = await _usuarioRepository.CadastrarAsync(usuario);

            if (tipoUsuarioEnum == Role.Medico && request.EspecialidadeIds != null)
            {
                var especialidadeIds = request.EspecialidadeIds
                    .Where(id => id.HasValue)
                    .Select(id => id!.Value)
                    .Distinct()
                    .ToList();

                if (especialidadeIds.Any())
                    await _usuarioRepository.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds);
            }

            response = "Usuário cadastrado com sucesso!";

            return response; 

        }
    }
}

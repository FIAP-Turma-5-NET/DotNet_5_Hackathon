using System.Runtime.CompilerServices;
using AutoMapper;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Model.Consulta;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Producer.Interface;
using Shared.Model;

namespace FIAP_HealthMed.Application.Service
{
    public class ConsultaApplicationService : IConsultaApplicationService
    {
        private readonly IConsultaDomainService _consultaDomainService;
        private readonly IMapper _mapper;
        private readonly IConsultaProducer _consultaProducer;

        public ConsultaApplicationService(IConsultaDomainService consultaDomainService, IMapper mapper, IConsultaProducer consultaProducer)
        {
            _consultaDomainService = consultaDomainService;
            _mapper = mapper;
            _consultaProducer = consultaProducer;
        }
        public async Task<string> AceitarConsulta(int consultaId, int usuarioId)
        {
            return await _consultaDomainService.AceitarConsultaAsync(consultaId, usuarioId);
        }

        public async Task<string> AgendarConsulta(ConsultaModelRequest request)
        {
            if (request.DataHora < DateTime.UtcNow)
                throw new InvalidOperationException("A data da consulta deve ser futura.");

            var mensagem = _mapper.Map<ConsultaMensagem>(request);
            mensagem.TipoEvento = "Cadastrar";

            await _consultaProducer.EnviarConsultaAsync(mensagem);

            return "Consulta enviada para processamento assíncrono com sucesso!";
        }

        public Task<string> CancelarConsulta(int consultaId, int usuarioId, string justificativa)
        {
            return _consultaDomainService.CancelarConsultaAsync(consultaId, usuarioId, justificativa);
        }

        public async Task<IEnumerable<ConsultaModelResponse>> ObterConsultas(int usuarioId, Role role)
        {
           var entities =  await _consultaDomainService.ObterConsultasPorUsuarioAsync(usuarioId, role);

            return _mapper.Map<IEnumerable<ConsultaModelResponse>>(entities);
        }

        public async Task<string> RecusarConsulta(int consultaId, int usuarioId)
        {
            return await _consultaDomainService.RecusarConsultaAsync(consultaId, usuarioId);
        }
    }
}

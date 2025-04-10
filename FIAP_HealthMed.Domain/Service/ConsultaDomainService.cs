using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Domain.Service
{
    public class ConsultaDomainService : IConsultaDomainService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IHorarioDisponivelRepository _horarioRepository;

        public ConsultaDomainService(IConsultaRepository consultaRepository, IHorarioDisponivelRepository horarioRepository)
        {
            _consultaRepository = consultaRepository;
            _horarioRepository = horarioRepository;
        }

        public async Task<string> AgendarConsultaAsync(Consulta consulta)
        {
            var response = string.Empty;

            // Verifica se o horário existe e está livre
            var horarios = await _horarioRepository.ObterPorMedicoIdAsync(consulta.MedicoId);
            var horarioDisponivel = horarios.FirstOrDefault(h => h.DataHora == consulta.DataHora && h.Ocupado == false);

            if (horarioDisponivel != null)
            {
                var result = await _consultaRepository.AgendarAsync(consulta);
                if (result > 0)
                {
                    horarioDisponivel.Ocupado = true;
                    await _horarioRepository.AtualizarHorarioAsync(horarioDisponivel.Id, horarioDisponivel.DataHora);
                    response = "Consulta agendada com sucesso!";
                }
                else
                {
                    throw new InvalidOperationException("Erro ao agendar consulta.");
                }
            }
            else
            {
                throw new InvalidOperationException("Horário indisponível para o médico.");
            }

            return response;
        }

        public async Task<string> CancelarConsultaAsync(int consultaId, string justificativa)
        {
            var response = string.Empty;

            var consulta = await _consultaRepository.ObterPorIdAsync(consultaId);

            if (consulta == null)
                throw new InvalidOperationException("Consulta não encontrada.");

            if (consulta.Status == StatusConsulta.Cancelada)
                throw new InvalidOperationException("Consulta já está cancelada.");

            var sucesso = await _consultaRepository
                .AtualizarStatusAsync(consultaId, StatusConsulta.Cancelada, justificativa);

            if (sucesso)
                response = "Consulta cancelada com sucesso.";
            else
                throw new InvalidOperationException("Erro ao cancelar consulta.");

            return response;
        }

        public async Task<string> AceitarConsultaAsync(int consultaId)
        {
            var response = string.Empty;

            var consulta = await _consultaRepository.ObterPorIdAsync(consultaId);

            if (consulta == null)
                throw new InvalidOperationException("Consulta não encontrada.");

            if (consulta.Status != StatusConsulta.Pendente)
                throw new InvalidOperationException("A consulta já foi processada.");

            var sucesso = await _consultaRepository
                .AtualizarStatusAsync(consultaId, StatusConsulta.Aceita);

            if (sucesso)
                response = "Consulta aceita com sucesso.";
            else
                throw new InvalidOperationException("Erro ao aceitar consulta.");

            return response;
        }

        public async Task<string> RecusarConsultaAsync(int consultaId)
        {
            var response = string.Empty;

            var consulta = await _consultaRepository.ObterPorIdAsync(consultaId);

            if (consulta == null)
                throw new InvalidOperationException("Consulta não encontrada.");

            if (consulta.Status != StatusConsulta.Pendente)
                throw new InvalidOperationException("A consulta já foi processada.");

            var sucesso = await _consultaRepository
                .AtualizarStatusAsync(consultaId, StatusConsulta.Recusada);

            if (sucesso)
                response = "Consulta recusada com sucesso.";
            else
                throw new InvalidOperationException("Erro ao recusar consulta.");

            return response;
        }

        public async Task<IEnumerable<Consulta>> ObterConsultasPorUsuarioAsync(int usuarioId, Role role)
        {
            var consultas = await _consultaRepository.ObterPorUsuarioIdAsync(usuarioId, role);
            return consultas;
        }
    }
}

using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Domain.Service
{
    public class ConsultaDomainService : IConsultaDomainService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IHorarioDisponivelRepository _horarioRepository;
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public ConsultaDomainService(
            IConsultaRepository consultaRepository, 
            IHorarioDisponivelRepository horarioRepository,
            IEspecialidadeRepository especialidadeRepository)
        {
            _consultaRepository = consultaRepository;
            _horarioRepository = horarioRepository;
            _especialidadeRepository = especialidadeRepository;
        }

        public async Task<string> AgendarConsultaAsync(Consulta consulta)
        {
            
            var horarios = await _horarioRepository.ObterPorMedicoIdAsync(consulta.MedicoId);
            var horario = horarios.FirstOrDefault(x => x.DataHora == consulta.DataHora && !x.Ocupado);
            if (horario == null)
                throw new InvalidOperationException("Horário indisponível.");
          
            var especialidade = await _especialidadeRepository.ObterPorIdAsync(consulta.EspecialidadeId);
            if (especialidade == null)
                throw new InvalidOperationException("Especialidade não encontrada.");
          
            consulta.ValorConsulta = especialidade.ValorConsulta;
            consulta.DataHora = horario.DataHora;
            consulta.MedicoId = horario.MedicoId;
          
            var id = await _consultaRepository.AgendarAsync(consulta);
            if (id <= 0)
                throw new InvalidOperationException("Erro ao agendar consulta.");

            horario.Ocupado = true;
            await _horarioRepository.AtualizarHorarioAsync(horario.Id, horario.DataHora);

            return "Consulta agendada com sucesso!";
        }

        public async Task<string> CancelarConsultaAsync(int consultaId, int usuarioId, string justificativa)
        {
            if (string.IsNullOrWhiteSpace(justificativa))
                throw new InvalidOperationException("Justificativa de cancelamento é obrigatória.");

            var consulta = await _consultaRepository.ObterPorIdAsync(consultaId);

            if (consulta == null)
                throw new InvalidOperationException("Consulta não encontrada.");

            if (consulta.PacienteId != usuarioId)
                throw new InvalidOperationException("Apenas o paciente pode cancelar a consulta.");

            if (consulta.Status == StatusConsulta.Cancelada)
                throw new InvalidOperationException("A consulta já está cancelada.");

            var sucesso = await _consultaRepository
                .AtualizarStatusAsync(consultaId, StatusConsulta.Cancelada, justificativa);

            return sucesso
                ? "Consulta cancelada com sucesso."
                : throw new InvalidOperationException("Erro ao cancelar a consulta.");
        }

        public async Task<string> AceitarConsultaAsync(int consultaId, int usuarioId)
        {
            var consulta = await _consultaRepository.ObterPorIdAsync(consultaId);

            if (consulta == null)
                throw new InvalidOperationException("Consulta não encontrada.");

            if (consulta.MedicoId != usuarioId)
                throw new InvalidOperationException("Apenas o médico responsável pode aceitar a consulta.");

            if (consulta.Status != StatusConsulta.Pendente)
                throw new InvalidOperationException("A consulta já foi processada.");

            var sucesso = await _consultaRepository
                .AtualizarStatusAsync(consultaId, StatusConsulta.Aceita);

            return sucesso
                ? "Consulta aceita com sucesso."
                : throw new InvalidOperationException("Erro ao aceitar consulta.");
        }

        public async Task<string> RecusarConsultaAsync(int consultaId, int usuarioId)
        {
            var consulta = await _consultaRepository.ObterPorIdAsync(consultaId);

            if (consulta == null)
                throw new InvalidOperationException("Consulta não encontrada.");

            if (consulta.MedicoId != usuarioId)
                throw new InvalidOperationException("Apenas o médico responsável pode recusar a consulta.");

            if (consulta.Status != StatusConsulta.Pendente)
                throw new InvalidOperationException("A consulta já foi processada.");

            var sucesso = await _consultaRepository
                .AtualizarStatusAsync(consultaId, StatusConsulta.Recusada);

            return sucesso
                ? "Consulta recusada com sucesso."
                : throw new InvalidOperationException("Erro ao recusar consulta.");
        }

        public async Task<IEnumerable<Consulta>> ObterConsultasPorUsuarioAsync(int usuarioId, Role role)
        {
            return role == Role.Medico 
                ? await _consultaRepository.ObterConsultasDoMedicoAsync(usuarioId)
                : await _consultaRepository.ObterConsultasDoPacienteAsync(usuarioId);
        }
    }
}

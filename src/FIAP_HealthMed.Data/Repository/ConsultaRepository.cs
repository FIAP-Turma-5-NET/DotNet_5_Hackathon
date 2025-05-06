using System.Data;
using Dapper;
using FIAP_HealthMed.Data.Context;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Data.Repository
{
    public class ConsultaRepository(IDbConnection context)
        : Context<Consulta>(context), IConsultaRepository
    {
        public async Task<int> AgendarAsync(Consulta consulta)
        {
            var p = new DynamicParameters();
            p.Add("@DataHora", consulta.DataHora);
            p.Add("@ValorConsulta", consulta.ValorConsulta);
            p.Add("@Status", (int)consulta.Status);
            p.Add("@JustificativaCancelamento", consulta.JustificativaCancelamento);
            p.Add("@MedicoId", consulta.MedicoId);
            p.Add("@PacienteId", consulta.PacienteId);
            p.Add("@EspecialidadeId", consulta.EspecialidadeId);

            var sql = @"
                        INSERT INTO Consulta
                          (DataHora, ValorConsulta, Status, JustificativaCancelamento, MedicoId, PacienteId, EspecialidadeId)
                        VALUES
                          (@DataHora, @ValorConsulta, @Status, @JustificativaCancelamento, @MedicoId, @PacienteId, @EspecialidadeId);
                        SELECT LAST_INSERT_ID();";

            return await context.ExecuteScalarAsync<int>(sql, p);
        }

        public async Task<bool> AtualizarStatusAsync(int consultaId, StatusConsulta status, string? justificativa = null)
        {
            var p = new DynamicParameters();
            p.Add("@Id", consultaId);
            p.Add("@Status", (int)status);
            p.Add("@Justificativa", justificativa);

            var sql = @"
                        UPDATE Consulta
                        SET Status = @Status,
                            JustificativaCancelamento = @Justificativa,
                            Updated_at = NOW()
                        WHERE Id = @Id
                          AND Deleted_at IS NULL;";

            var linhasAfetadas = await context.ExecuteAsync(sql, p);
            return linhasAfetadas > 0;
        }

        public async Task<Consulta?> ObterPorIdAsync(int id)
        {
            var sql = @"
                        SELECT DISTINCT
                            c.Id,
                            c.DataHora,
                            c.ValorConsulta,
                            c.Status,
                            c.JustificativaCancelamento,
                            c.MedicoId,
                            m.Nome            AS MedicoNome,
                            c.PacienteId,
                            p.Nome            AS PacienteNome,
                            c.EspecialidadeId,
                            e.Nome            AS NomeEspecialidade
                        FROM Consulta c
                        LEFT JOIN Usuario       m ON m.Id = c.MedicoId
                        LEFT JOIN Usuario       p ON p.Id = c.PacienteId
                        LEFT JOIN Especialidade e ON e.Id = c.EspecialidadeId
                        WHERE c.Id = @Id
                          AND c.Deleted_at IS NULL;";

            return await context.QueryFirstOrDefaultAsync<Consulta>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Consulta>> ObterConsultasDoMedicoAsync(int medicoId)
        {
            var sql = @"
                       SELECT c.Id, c.DataHora, c.ValorConsulta, c.Status, c.JustificativaCancelamento,
                              c.MedicoId, m.Nome AS MedicoNome,
                              c.PacienteId, p.Nome AS PacienteNome,
                              c.EspecialidadeId, e.Nome AS NomeEspecialidade
                       FROM Consulta c
                       LEFT JOIN Usuario m ON m.Id = c.MedicoId
                       LEFT JOIN Usuario p ON p.Id = c.PacienteId
                       LEFT JOIN Especialidade e ON e.Id = c.EspecialidadeId
                       WHERE c.MedicoId = @MedicoId AND c.Deleted_at IS NULL";

            return await context.QueryAsync<Consulta>(sql, new { MedicoId = medicoId });
        }

        public async Task<IEnumerable<Consulta>> ObterConsultasDoPacienteAsync(int pacienteId)
        {
            var sql = @"
                       SELECT c.Id, c.DataHora, c.ValorConsulta, c.Status, c.JustificativaCancelamento,
                              c.MedicoId, m.Nome AS MedicoNome,
                              c.PacienteId, p.Nome AS PacienteNome,
                              c.EspecialidadeId, e.Nome AS NomeEspecialidade
                       FROM Consulta c
                       LEFT JOIN Usuario m ON m.Id = c.MedicoId
                       LEFT JOIN Usuario p ON p.Id = c.PacienteId
                       LEFT JOIN Especialidade e ON e.Id = c.EspecialidadeId
                       WHERE c.PacienteId = @PacienteId AND c.Deleted_at IS NULL";

            return await context.QueryAsync<Consulta>(sql, new { PacienteId = pacienteId });
        }


    }
}

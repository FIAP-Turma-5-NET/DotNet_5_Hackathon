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
            var parametros = new DynamicParameters();
            parametros.Add("@DataHora", consulta.DataHora);
            parametros.Add("@Valor", consulta.Valor);
            parametros.Add("@Status", (int)consulta.Status);
            parametros.Add("@JustificativaCancelamento", consulta.JustificativaCancelamento);
            parametros.Add("@MedicoId", consulta.MedicoId);
            parametros.Add("@PacienteId", consulta.PacienteId);

            var sql = @"INSERT INTO Consulta (DataHora, Valor, Status, JustificativaCancelamento, MedicoId, PacienteId)
                        VALUES (@DataHora, @Valor, @Status, @JustificativaCancelamento, @MedicoId, @PacienteId);
                        SELECT LAST_INSERT_ID();";

            var id = await context.ExecuteScalarAsync<int>(sql, parametros);
            return id;
        }

        public async Task<bool> AtualizarStatusAsync(int consultaId, StatusConsulta status, string? justificativa = null)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Id", consultaId);
            parametros.Add("@Status", (int)status);
            parametros.Add("@Justificativa", justificativa);

            var sql = @"UPDATE Consulta 
                        SET Status = @Status, 
                            JustificativaCancelamento = @Justificativa,
                            Updated_at = NOW()
                        WHERE Id = @Id AND Deleted_at IS NULL";

            var linhasAfetadas = await context.ExecuteAsync(sql, parametros);
            return linhasAfetadas > 0;
        }

        public async Task<Consulta?> ObterPorIdAsync(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Id", id);

            var sql = "SELECT * FROM Consulta WHERE Id = @Id AND Deleted_at IS NULL";

            return await context.QueryFirstOrDefaultAsync<Consulta>(sql, parametros);
        }

        public async Task<IEnumerable<Consulta>> ObterPorUsuarioIdAsync(int usuarioId, Role role)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Id", usuarioId);

            var sql = role == Role.Medico
                ? "SELECT * FROM Consulta WHERE MedicoId = @Id AND Deleted_at IS NULL"
                : "SELECT * FROM Consulta WHERE PacienteId = @Id AND Deleted_at IS NULL";

            return await context.QueryAsync<Consulta>(sql, parametros);
        }
    }
}

using System.Data;
using Dapper;
using FIAP_HealthMed.Data.Context;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Data.Repository
{
    public class HorarioDisponivelRepository(IDbConnection context)
        : Context<HorarioDisponivel>(context), IHorarioDisponivelRepository
    {
        public async Task<IEnumerable<HorarioDisponivel>> ObterPorMedicoIdAsync(int medicoId)
        {
            var sql = "SELECT * FROM HorarioDisponivel WHERE MedicoId = @MedicoId AND Deleted_at IS NULL";
            return await context.QueryAsync<HorarioDisponivel>(sql, new { MedicoId = medicoId });
        }

        public async Task<bool> InserirHorariosAsync(IEnumerable<HorarioDisponivel> horarios)
        {
            foreach (var h in horarios)
            {
                var parametros = new DynamicParameters();
                parametros.Add("@DataHora", h.DataHora);
                parametros.Add("@Ocupado", h.Ocupado);
                parametros.Add("@MedicoId", h.MedicoId);

                var sql = @"INSERT INTO HorarioDisponivel (DataHora, Ocupado, MedicoId)
                            VALUES (@DataHora, @Ocupado, @MedicoId);";

                var linhas = await context.ExecuteAsync(sql, parametros);
                if (linhas <= 0) return false;
            }
            return true;
        }

        public async Task<bool> AtualizarHorarioAsync(int horarioId, DateTime novoHorario)
        {
            var sql = @"UPDATE HorarioDisponivel SET DataHora = @DataHora, Ocupado = 1, Updated_at = NOW()
                        WHERE Id = @Id AND Deleted_at IS NULL";

            var result = await context.ExecuteAsync(sql, new { Id = horarioId, DataHora = novoHorario });
            return result > 0;
        }

        public async Task<bool> RemoverHorarioAsync(int horarioId)
        {
            var sql = "UPDATE HorarioDisponivel SET Deleted_at = NOW() WHERE Id = @Id";
            var result = await context.ExecuteAsync(sql, new { Id = horarioId });
            return result > 0;
        }
    }
}

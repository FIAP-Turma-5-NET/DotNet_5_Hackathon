using System.Data;
using Dapper;
using FIAP_HealthMed.Data.Context;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Data.Repository
{
    public class EspecialidadeRepository(IDbConnection context)
        : Context<Especialidade>(context), IEspecialidadeRepository
    {
        public async Task<int> CadastrarAsync(Especialidade especialidade)
        {
            var sql = @"INSERT INTO Especialidade (Nome, ValorConsulta) VALUES (@Nome, @ValorConsulta);
                        SELECT LAST_INSERT_ID();";

            var parametros = new DynamicParameters();
            parametros.Add("@Nome", especialidade.Nome);
            parametros.Add("@ValorConsulta", especialidade.ValorConsulta);

            return await context.ExecuteScalarAsync<int>(sql, parametros);
        }

        public async Task<IEnumerable<Especialidade>> ObterTodasAsync()
        {
            var sql = "SELECT * FROM Especialidade WHERE Deleted_at IS NULL";
            return await context.QueryAsync<Especialidade>(sql);
        }

        public async Task<Especialidade?> ObterPorIdAsync(int id)
        {
            var sql = "SELECT * FROM Especialidade WHERE Id = @Id AND Deleted_at IS NULL";
            return await context.QueryFirstOrDefaultAsync<Especialidade>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Especialidade>> ObterPorIdsAsync(IEnumerable<int?> ids)
        {
            var sql = "SELECT * FROM Especialidade WHERE Id IN @Ids AND Deleted_at IS NULL";
            return await context.QueryAsync<Especialidade>(sql, new { Ids = ids });
        }
    }
}

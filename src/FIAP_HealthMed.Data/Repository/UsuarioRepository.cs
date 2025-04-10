using System.Data;
using Dapper;
using FIAP_HealthMed.Data.Context;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Interface.Repository;

namespace FIAP_HealthMed.Data.Repository
{
    public class UsuarioRepository(IDbConnection context)
         : Context<Usuario>(context), IUsuarioRepository
    {
        public async Task<int> CadastrarAsync(Usuario usuario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Nome", usuario.Nome);
            parametros.Add("@CPF", usuario.CPF);
            parametros.Add("@Email", usuario.Email);
            parametros.Add("@SenhaHash", usuario.SenhaHash);
            parametros.Add("@DDD", usuario.DDD);
            parametros.Add("@Telefone", usuario.Telefone);
            parametros.Add("@Role", (int)usuario.Role);
            parametros.Add("@CRM", usuario.CRM);
            parametros.Add("@EspecialidadeId", usuario.EspecialidadeId);

            var sql = @"INSERT INTO Usuario (Nome, CPF, Email, SenhaHash, DDD, Telefone, Role, CRM, EspecialidadeId)
                        VALUES (@Nome, @CPF, @Email, @SenhaHash, @DDD, @Telefone, @Role, @CRM, @EspecialidadeId);
                        SELECT LAST_INSERT_ID();";

            return await context.ExecuteScalarAsync<int>(sql, parametros);
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            var sql = "SELECT * FROM Usuario WHERE Id = @Id AND Deleted_at IS NULL";
            return await context.QueryFirstOrDefaultAsync<Usuario>(sql, new { Id = id });
        }

        public async Task<Usuario?> ObterPorCpfOuEmailAsync(string login)
        {
            var sql = "SELECT * FROM Usuario WHERE (CPF = @Login OR Email = @Login) AND Deleted_at IS NULL";
            return await context.QueryFirstOrDefaultAsync<Usuario>(sql, new { Login = login });
        }

        public async Task<bool> VerificarExistentePorCpfOuEmailAsync(string cpf, string email)
        {
            var sql = "SELECT COUNT(1) FROM Usuario WHERE (CPF = @Cpf OR Email = @Email) AND Deleted_at IS NULL";
            var result = await context.ExecuteScalarAsync<int>(sql, new { Cpf = cpf, Email = email });
            return result > 0;
        }

        public async Task<IEnumerable<Usuario>> ListarMedicosAsync(string? especialidade = null)
        {
            var sql = "SELECT * FROM Usuario WHERE Role = 0 AND Deleted_at IS NULL";

            if (!string.IsNullOrEmpty(especialidade))
            {
                sql += " AND EspecialidadeId = @EspecialidadeId";
                return await context.QueryAsync<Usuario>(sql, new { EspecialidadeId = int.Parse(especialidade) });
            }

            return await context.QueryAsync<Usuario>(sql);
        }
    }
}

using System.Data;
using Dapper;
using FIAP_HealthMed.Data.Context;
using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
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

            var sql = @"INSERT INTO Usuario (Nome, CPF, Email, SenhaHash, DDD, Telefone, Role, CRM)
                VALUES (@Nome, @CPF, @Email, @SenhaHash, @DDD, @Telefone, @Role, @CRM);
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
        
        public async Task<IEnumerable<Usuario>> ListarMedicosAsync(int? especialidadeId = null, string? nome = null, string? crm = null)
        {
            var sql = @"
                SELECT DISTINCT
                    u.Id,
                    u.Nome,
                    u.CPF,
                    u.DDD,
                    u.Telefone,
                    u.Email,
                    u.SenhaHash,
                    u.Role,
                    u.CRM,
                    u.Ativo,
                    e.Id AS Id,
                    e.Nome AS Nome
                FROM Usuario u
                LEFT JOIN Usuario_Especialidade ue ON ue.UsuarioId = u.Id
                LEFT JOIN Especialidade e ON e.Id = ue.EspecialidadeId
                WHERE u.Role = @role
                AND u.Ativo = 1
                AND u.Deleted_at IS NULL";

            var parametros = new DynamicParameters();
            parametros.Add("@role", Role.Medico);

            if (especialidadeId.HasValue)
            {
                sql += " AND ue.EspecialidadeId = @especialidadeId";
                parametros.Add("@especialidadeId", especialidadeId.Value);
            }

            if (!string.IsNullOrWhiteSpace(nome))
            {
                sql += " AND u.Nome LIKE @nome";
                parametros.Add("@nome", $"%{nome}%");
            }

            if (!string.IsNullOrWhiteSpace(crm))
            {
                sql += " AND u.CRM = @crm";
                parametros.Add("@crm", crm);
            }

            var dict = new Dictionary<int, Usuario>();

            await context.QueryAsync<Usuario, Especialidade, Usuario>(
                sql,
                (usuario, especialidade) =>
                {
                    if (!dict.TryGetValue(usuario.Id, out var uEntry))
                    {
                        uEntry = usuario;
                        uEntry.Especialidades = new List<Especialidade>();
                        dict.Add(uEntry.Id, uEntry);
                    }
                    if (especialidade != null && especialidade.Id != 0)
                    {
                        uEntry.Especialidades.Add(especialidade);
                    }
                    return uEntry;
                },
                parametros,
                splitOn: "Id"
            );

            return dict.Values;
        }


        public async Task InserirEspecialidadesUsuarioAsync(int usuarioId, IEnumerable<int> especialidadeIds)
        {
            var sql = @"INSERT INTO Usuario_Especialidade (UsuarioId, EspecialidadeId)
                        VALUES (@UsuarioId, @EspecialidadeId);";

            foreach(var especialidadeId in especialidadeIds)
            {
                await context.ExecuteAsync(sql, new { UsuarioId = usuarioId, EspecialidadeId = especialidadeId });
            }
        }

      
    }
}

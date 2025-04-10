using System.Data;
using Dapper.Contrib.Extensions;
using FIAP_HealthMed.Domain.Entity;
using MySql.Data.MySqlClient;

namespace FIAP_HealthMed.Data.Context
{
    public class Context<T> where T : EntityBase
    {
        protected IDbConnection _context;

        public Context(IDbConnection context) => _context = context;

        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(_context.ConnectionString);
            }
        }

        public async Task<int> CadastrarAsync(T entity)
        {
            using (var con = Connection)
                return await _context.InsertAsync<T>(entity);
        }

        public async Task<bool> AtualizarAsync(T entity)
        {
            using (var con = Connection)
                return await _context.UpdateAsync<T>(entity);
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            using (var con = Connection)
                return await _context.GetAllAsync<T>();
        }

        public async Task<bool> DeletarAsync(T entity)
        {
            using (var con = Connection)
                return await _context.DeleteAsync<T>(entity);
        }
    }
}

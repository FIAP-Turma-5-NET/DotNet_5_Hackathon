using FIAP_HealthMed.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_HealthMed.Application.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
        //string GerarToken(string login, string role, DateTime dataExpiracao);
        //string GerarToken(string login, string role, DateTime dataExpiracao, IEnumerable<string> permissoes);
        //string GerarToken(string login, string role, DateTime dataExpiracao, IEnumerable<string> permissoes, IEnumerable<string> claims);
        //bool ValidarToken(string token);
        //string ObterLoginDoToken(string token);
        //string ObterRoleDoToken(string token);
        //DateTime ObterDataExpiracaoDoToken(string token);
        //IEnumerable<string> ObterPermissoesDoToken(string token);
    }
}

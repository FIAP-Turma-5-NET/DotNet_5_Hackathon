using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Model;

namespace FIAP_HealthMed.Consumer.Interface
{
    public interface IUsuarioConsumerService
    {
        Task<string> CadastrarUsuario(UsuarioMensagem request);       
    }
}

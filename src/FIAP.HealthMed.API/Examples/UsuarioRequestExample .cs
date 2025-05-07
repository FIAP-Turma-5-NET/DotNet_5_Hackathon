using FIAP_HealthMed.Application.Model.Usuario;

using Google.Protobuf.WellKnownTypes;

using Swashbuckle.AspNetCore.Filters;

namespace FIAP.HealthMed.API.Examples
{


    public class UsuarioRequestExample : IExamplesProvider<UsuarioModelRequest>
    {
        public UsuarioModelRequest GetExamples()
        {
            return new UsuarioModelRequest
            {
                Nome = "João Silva",
                CPF = "12345678900",
                Email = "joao@example.com",
                Senha = "senha123",
                DDD = "11",
                Telefone = "999999999",
                TipoUsuario = "medico/paciente",
                Ativo = true,
                CRM = "CRM-SP-12345",
                EspecialidadeIds = new List<int?>()
            };
        }
    }

}

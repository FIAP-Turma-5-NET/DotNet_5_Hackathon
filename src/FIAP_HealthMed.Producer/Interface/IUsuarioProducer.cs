using Shared.Model;

namespace FIAP_HealthMed.Producer.Interface
{
    public interface IUsuarioProducer
    {
        Task EnviarUsuarioAsync(UsuarioMensagem mensagem);
    }
}

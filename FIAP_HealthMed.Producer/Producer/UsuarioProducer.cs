using FIAP_HealthMed.Producer.Interface;
using Shared.Model;

namespace FIAP_HealthMed.Producer.Producer
{
    public class UsuarioProducer : IUsuarioProducer
    {
        private readonly IProducerService _producerService;
        private readonly string _queueName;

        public UsuarioProducer(IProducerService producerService)
        {
            _producerService = producerService;
            _queueName = Environment.GetEnvironmentVariable("MassTransit_Filas_UsuarioFila") ?? string.Empty;
        }
        public async Task EnviarUsuarioAsync(UsuarioMensagem mensagem)
        {
            await _producerService.EnviaMensagemAsync(mensagem, _queueName + "-" + mensagem.TipoEvento);
        }
    }
}

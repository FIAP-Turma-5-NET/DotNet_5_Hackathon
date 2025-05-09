using FIAP_HealthMed.Producer.Interface;
using Shared.Model;

namespace FIAP_HealthMed.Producer.Producer
{
    public class ConsultaProducer : IConsultaProducer
    {
        private readonly IProducerService _producerService;
        private readonly string _queueName;

        public ConsultaProducer(IProducerService producerService)
        {
            _producerService = producerService;
            _queueName = Environment.GetEnvironmentVariable("MassTransit_Filas_ConsultaFila") ?? string.Empty;
        }
        public async Task EnviarConsultaAsync(ConsultaMensagem mensagem)
        {
            await _producerService.EnviaMensagemAsync(mensagem, _queueName + "-" + mensagem.TipoEvento);
        }
    }
}

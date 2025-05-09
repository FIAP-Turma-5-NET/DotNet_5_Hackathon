using FIAP_HealthMed.Consumer.Interface;
using MassTransit;
using Shared.Model;

namespace FIAP_HealthMed.Consumer.Consumer
{
    public class UsuarioConsumer : IConsumer<UsuarioMensagem>
    {
        private readonly IUsuarioConsumerService _consumerService;
        public UsuarioConsumer(IUsuarioConsumerService consumerService)
        {
            _consumerService = consumerService;
        }
        public async Task Consume(ConsumeContext<UsuarioMensagem> context)
        {
            try
            {
                switch (context.Message.TipoEvento)
                {
                    case "Cadastrar":
                        await _consumerService.CadastrarUsuario(context.Message);
                        break;
                    case "Atualizar":
                        //await _consumerService.AtualizarUsuario(context.Message);
                        break;
                    case "Deletar":
                        //await _consumerService.DeletarUsuario(context.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
            }
        }
    }
}

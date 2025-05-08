using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_HealthMed.Producer.Interface
{
    public interface IProducerService
    {
        Task EnviaMensagemAsync<T>(T mensagem, string queueName) where T : class;
    }
}

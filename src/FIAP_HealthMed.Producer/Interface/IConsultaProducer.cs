﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Model;

namespace FIAP_HealthMed.Producer.Interface
{
    public interface IConsultaProducer
    {
        Task EnviarConsultaAsync(ConsultaMensagem mensagem);
    }
}

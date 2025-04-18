﻿using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Application.Model.Usuario
{
    public class UsuarioModelRequest
    {
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string DDD { get; set; }
        public required string Telefone { get; set; }
        public required Role Role { get; set; }

        // Só se for Médico
        public string? CRM { get; set; }
        public int? EspecialidadeId { get; set; }
    }
}

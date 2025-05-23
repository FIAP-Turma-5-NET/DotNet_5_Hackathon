﻿using FIAP_HealthMed.Domain.Enums;

namespace FIAP_HealthMed.Application.Model.Consulta
{
    public record ConsultaModelResponse
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public decimal ValorConsulta { get; set; }
        public StatusConsulta Status { get; set; }
        public string? JustificativaCancelamento { get; set; }
        public int MedicoId { get; set; }
        public string? NomeMedico { get; set; }
        public int PacienteId { get; set; }
        public string? NomePaciente { get; set; }
        public int EspecialidadeId { get; init; }           
        public string? NomeEspecialidade { get; init; }
    }
}

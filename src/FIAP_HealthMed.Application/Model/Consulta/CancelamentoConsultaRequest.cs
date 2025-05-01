namespace FIAP_HealthMed.Application.Model.Consulta
{
    public record CancelamentoConsultaRequest
    {
        public int UsuarioId { get; set; }
        public string Justificativa { get; set; } = string.Empty;
    }
}

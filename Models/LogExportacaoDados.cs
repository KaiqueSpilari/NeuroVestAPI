namespace NeuroVestAPI.Models
{
    public class LogExportacaoDados
    {
        public Guid LogExportacaoDadosId { get; set; }
        public Guid LoginId { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime DataHoraSolicitacao { get; set; }
        public string FormatoArquivo { get; set; } = string.Empty;
        public StatusExportacao StatusOperacao { get; set; }

        public Login Login { get; set; } = null!;
        public PerfilPaciente PerfilPaciente { get; set; } = null!;
    }
}
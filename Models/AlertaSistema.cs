namespace NeuroVestAPI.Models
{
    public class AlertaSistema
    {
        public Guid AlertaSistemaId { get; set; }
        public Guid LoginId { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime DataHora { get; set; }
        public string TipoSensor { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public CriticidadeAlerta Criticidade { get; set; }

        public Login Login { get; set; } = null!;
        public PerfilPaciente PerfilPaciente { get; set; } = null!;
    }
}
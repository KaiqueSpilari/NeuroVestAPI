namespace NeuroVestAPI.Models
{
    public class LogInteracaoIA
    {
        public Guid LogInteracaoIAId { get; set; }
        public Guid LoginId { get; set; }
        public Guid? ContextoPacienteId { get; set; }
        public string PerguntaUsuario { get; set; } = string.Empty;
        public string RespostaIa { get; set; } = string.Empty;
        public int TokensConsumidos { get; set; }
        public DateTime DataHoraMensagem { get; set; }

        public Login Login { get; set; } = null!;
        public PerfilPaciente? PerfilPaciente { get; set; }
    }
}
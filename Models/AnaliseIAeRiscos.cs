namespace NeuroVestAPI.Models
{
    public class AnaliseIAeRiscos
    {
        public Guid AnaliseIAeRiscosId { get; set; }
        public Guid LoginId { get; set; }
        public Guid PacienteId { get; set; }
        public int ScoreGeral { get; set; }
        public int RiscoVascular { get; set; }
        public int RiscoCerebral { get; set; }
        public int RiscoCardiaco { get; set; }
        public string ClassificacaoTexto { get; set; } = string.Empty;

        public Login Login { get; set; } = null!;
        public PerfilPaciente PerfilPaciente { get; set; } = null!;
    }
}
namespace NeuroVestAPI.Models
{
    public class SessaoTelemetria
    {
        public long Id { get; set; }
        public Guid LoginId { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime DataHora { get; set; }
        public int FcBpm { get; set; }
        public int PaSistolica { get; set; }
        public int PaDiastolica { get; set; }
        public int Spo2 { get; set; }
        public int VfcRmssd { get; set; }
        public decimal AmplitudeEcg { get; set; }
        public int FluxoCerebral { get; set; }
        public int OxigCerebralFnirs { get; set; }
        public decimal IndiceAlfaBeta { get; set; }
        public StatusGeral StatusGeral { get; set; }

        public Login Login { get; set; } = null!;
        public PerfilPaciente PerfilPaciente { get; set; } = null!;
    }
}
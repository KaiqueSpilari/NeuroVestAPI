namespace NeuroVestAPI.Models
{
    public class MetricasOndasEEG
    {
        public long Id { get; set; }
        public Guid LoginId { get; set; }
        public long SessaoId { get; set; }
        public int Delta { get; set; }
        public int Theta { get; set; }
        public int Alpha { get; set; }
        public int Beta { get; set; }
        public int Gamma { get; set; }

        public Login Login { get; set; } = null!;
        public SessaoTelemetria SessaoTelemetria { get; set; } = null!;
    }
}
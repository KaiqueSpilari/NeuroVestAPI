namespace NeuroVestAPI.Models
{
    public class SessaoECGRawData
    {
        public long Id { get; set; }
        public Guid LoginId { get; set; }
        public long SessaoId { get; set; }
        public int FrequenciaAmostragemHz { get; set; }
        public string ValoresSinalMv { get; set; } = string.Empty;

        public Login Login { get; set; } = null!;
        public SessaoTelemetria SessaoTelemetria { get; set; } = null!;
    }
}
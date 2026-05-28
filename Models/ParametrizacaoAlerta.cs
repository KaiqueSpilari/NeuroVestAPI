namespace NeuroVestAPI.Models
{
    public class ParametrizacaoAlerta
    {
        public Guid ParametrizacaoAlertaId { get; set; }
        public Guid LoginId { get; set; }
        public string MetricaAlvo { get; set; } = string.Empty;
        public decimal ValorMinimoToleravel { get; set; }
        public decimal ValorMaximoToleravel { get; set; }
        public string MensagemCustomizada { get; set; } = string.Empty;

        public Login Login { get; set; } = null!;
    }
}
namespace NeuroVestAPI.Models
{
    public class HistoricoManutencaoDispositivo
    {
        public Guid HistoricoManutencaoDispositivoId { get; set; }
        public Guid LoginId { get; set; }
        public Guid DispositivoId { get; set; }
        public DateTime DataManutencao { get; set; }
        public TipoIntervencao TipoIntervencao { get; set; }
        public string DescricaoDetalhada { get; set; } = string.Empty;

        public Login Login { get; set; } = null!;
        public Dispositivo Dispositivo { get; set; } = null!;
    }
}
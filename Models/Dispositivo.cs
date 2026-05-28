namespace NeuroVestAPI.Models
{
    public class Dispositivo
    {
        public Guid DispositivoId { get; set; }
        public Guid LoginId { get; set; }
        public string CodigoHardware { get; set; } = string.Empty;
        public string NomeDispositivo { get; set; } = string.Empty;
        public TipoDispositivo Tipo { get; set; }
        public string StatusOperacional { get; set; } = string.Empty;
        public int BateriaPorcentagem { get; set; }
        public int SinalPorcentagem { get; set; }
        public ModoFuncionamento ModoFuncionamento { get; set; }

        public Login Login { get; set; } = null!;
    }
}
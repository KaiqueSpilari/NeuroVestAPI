namespace NeuroVestAPI.Models
{
    public class PerfilMedico
    {
        public Guid PerfilMedicoId { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Crm { get; set; } = string.Empty;

        public Guid LoginId { get; set; }
        public Login Login { get; set; } = null!;
    }
}
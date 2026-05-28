namespace NeuroVestAPI.Models
{
    public class Login
    {
        public Guid LoginId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime DataCriacao { get; set; }

        public PerfilMedico? PerfilMedico { get; set; }
        public PerfilPaciente? PerfilPaciente { get; set; }
    }
}
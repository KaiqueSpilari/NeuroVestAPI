namespace NeuroVestAPI.Models
{
    public class LoginCreateRequest
    {
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string TipoUsuario { get; set; } = string.Empty;
        public DateTime? DataCriacao { get; set; }
    }
}
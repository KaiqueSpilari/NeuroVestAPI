namespace NeuroVestAPI.Models
{
    public class CondicaoPreExistente
    {
        public Guid CondicaoPreExistenteId { get; set; }
        public Guid LoginId { get; set; }
        public Guid PacienteId { get; set; }
        public string NomeCondicao { get; set; } = string.Empty;

        public Login Login { get; set; } = null!;
        public PerfilPaciente PerfilPaciente { get; set; } = null!;
    }
}
namespace NeuroVestAPI.Models
{
	public class PerfilPaciente
	{
		public Guid PerfilPacienteId { get; set; }
		public Guid LoginId { get; set; }
		public Guid? MedicoResponsavelId { get; set; }
		public string CodigoPaciente { get; set; } = string.Empty;
		public string NomeCompleto { get; set; } = string.Empty;
		public int Idade { get; set; }
		public string Sexo { get; set; } = string.Empty;
		public decimal PesoKg { get; set; }
		public int AlturaCm { get; set; }
		public decimal Imc { get; set; }

		public Login Login { get; set; } = null!;
		public PerfilMedico? MedicoResponsavel { get; set; }
	}
}

namespace NeuroVestAPI.Models
{
    public class RecomendacaoSistema
    {
        public Guid RecomendacaoSistemaId { get; set; }
        public Guid LoginId { get; set; }
        public Guid AnaliseId { get; set; }
        public CategoriaRecomendacao Categoria { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public Login Login { get; set; } = null!;
        public AnaliseIAeRiscos AnaliseIAeRiscos { get; set; } = null!;
    }
}
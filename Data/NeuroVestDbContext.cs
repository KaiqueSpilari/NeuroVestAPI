using Microsoft.EntityFrameworkCore;
using NeuroVestAPI.Models;

namespace NeuroVestAPI.Data
{
    public class NeuroVestDbContext : DbContext
    {
        public NeuroVestDbContext(DbContextOptions<NeuroVestDbContext> options) : base(options)
        {
        }

        public DbSet<Login> Logins => Set<Login>();
        public DbSet<PerfilMedico> PerfisMedico => Set<PerfilMedico>();
        public DbSet<PerfilPaciente> PerfisPaciente => Set<PerfilPaciente>();
        public DbSet<CondicaoPreExistente> CondicoesPreExistentes => Set<CondicaoPreExistente>();
        public DbSet<Dispositivo> Dispositivos => Set<Dispositivo>();
        public DbSet<SessaoTelemetria> SessoesTelemetria => Set<SessaoTelemetria>();
        public DbSet<MetricasOndasEEG> MetricasOndasEEG => Set<MetricasOndasEEG>();
        public DbSet<AnaliseIAeRiscos> AnalisesIAeRiscos => Set<AnaliseIAeRiscos>();
        public DbSet<RecomendacaoSistema> RecomendacoesSistema => Set<RecomendacaoSistema>();
        public DbSet<AlertaSistema> AlertasSistema => Set<AlertaSistema>();
        public DbSet<LogExportacaoDados> LogsExportacaoDados => Set<LogExportacaoDados>();
        public DbSet<ParametrizacaoAlerta> ParametrizacoesAlerta => Set<ParametrizacaoAlerta>();
        public DbSet<HistoricoManutencaoDispositivo> HistoricosManutencaoDispositivo => Set<HistoricoManutencaoDispositivo>();
        public DbSet<LogInteracaoIA> LogsInteracaoIA => Set<LogInteracaoIA>();
        public DbSet<SessaoECGRawData> SessoesECGRawData => Set<SessaoECGRawData>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("login");
                entity.HasKey(x => x.LoginId);
                entity.Property(x => x.Email).HasMaxLength(100).IsRequired();
                entity.HasIndex(x => x.Email).IsUnique();
                entity.Property(x => x.SenhaHash).HasMaxLength(255).IsRequired();
                entity.Property(x => x.TipoUsuario).HasConversion<string>().HasMaxLength(20).IsRequired();
                entity.Property(x => x.DataCriacao).IsRequired();
            });

            modelBuilder.Entity<PerfilMedico>(entity =>
            {
                entity.ToTable("perfil_medico");
                entity.HasKey(x => x.PerfilMedicoId);
                entity.Property(x => x.NomeCompleto).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Crm).HasMaxLength(20).IsRequired();
                entity.HasIndex(x => x.Crm).IsUnique();
                entity.HasIndex(x => x.LoginId).IsUnique();
                entity.HasOne(x => x.Login)
                    .WithOne(x => x.PerfilMedico)
                    .HasForeignKey<PerfilMedico>(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PerfilPaciente>(entity =>
            {
                entity.ToTable("perfil_paciente");
                entity.HasKey(x => x.PerfilPacienteId);
                entity.Property(x => x.CodigoPaciente).HasMaxLength(20).IsRequired();
                entity.Property(x => x.NomeCompleto).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Sexo).HasMaxLength(20).IsRequired();
                entity.Property(x => x.PesoKg).HasPrecision(5, 2);
                entity.Property(x => x.Imc).HasPrecision(4, 1);
                entity.HasIndex(x => x.CodigoPaciente).IsUnique();
                entity.HasIndex(x => x.LoginId).IsUnique();
                entity.HasOne(x => x.Login)
                    .WithOne(x => x.PerfilPaciente)
                    .HasForeignKey<PerfilPaciente>(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.MedicoResponsavel)
                    .WithMany()
                    .HasForeignKey(x => x.MedicoResponsavelId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CondicaoPreExistente>(entity =>
            {
                entity.ToTable("condicao_pre_existente");
                entity.HasKey(x => x.CondicaoPreExistenteId);
                entity.Property(x => x.NomeCondicao).HasMaxLength(100).IsRequired();
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.PerfilPaciente)
                    .WithMany()
                    .HasForeignKey(x => x.PacienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Dispositivo>(entity =>
            {
                entity.ToTable("dispositivo");
                entity.HasKey(x => x.DispositivoId);
                entity.Property(x => x.CodigoHardware).HasMaxLength(50);
                entity.Property(x => x.NomeDispositivo).HasMaxLength(50);
                entity.Property(x => x.Tipo).HasConversion<string>().HasMaxLength(20).IsRequired();
                entity.Property(x => x.StatusOperacional).HasMaxLength(50);
                entity.Property(x => x.ModoFuncionamento).HasConversion<string>().HasMaxLength(20).IsRequired();
                entity.HasIndex(x => x.CodigoHardware).IsUnique();
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SessaoTelemetria>(entity =>
            {
                entity.ToTable("sessao_telemetria");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.AmplitudeEcg).HasPrecision(4, 2);
                entity.Property(x => x.IndiceAlfaBeta).HasPrecision(3, 1);
                entity.Property(x => x.StatusGeral).HasConversion<string>().HasMaxLength(20).IsRequired();
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.PerfilPaciente)
                    .WithMany()
                    .HasForeignKey(x => x.PacienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MetricasOndasEEG>(entity =>
            {
                entity.ToTable("metricas_ondas_eeg");
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.SessaoTelemetria)
                    .WithMany()
                    .HasForeignKey(x => x.SessaoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AnaliseIAeRiscos>(entity =>
            {
                entity.ToTable("analise_ia_e_riscos");
                entity.HasKey(x => x.AnaliseIAeRiscosId);
                entity.Property(x => x.ClassificacaoTexto).HasMaxLength(50);
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.PerfilPaciente)
                    .WithMany()
                    .HasForeignKey(x => x.PacienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RecomendacaoSistema>(entity =>
            {
                entity.ToTable("recomendacao_sistema");
                entity.HasKey(x => x.RecomendacaoSistemaId);
                entity.Property(x => x.Categoria).HasConversion<string>().HasMaxLength(30).IsRequired();
                entity.Property(x => x.Descricao).HasColumnType("text");
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.AnaliseIAeRiscos)
                    .WithMany()
                    .HasForeignKey(x => x.AnaliseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AlertaSistema>(entity =>
            {
                entity.ToTable("alerta_sistema");
                entity.HasKey(x => x.AlertaSistemaId);
                entity.Property(x => x.TipoSensor).HasMaxLength(50);
                entity.Property(x => x.Mensagem).HasMaxLength(255);
                entity.Property(x => x.Criticidade).HasConversion<string>().HasMaxLength(20).IsRequired();
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.PerfilPaciente)
                    .WithMany()
                    .HasForeignKey(x => x.PacienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LogExportacaoDados>(entity =>
            {
                entity.ToTable("log_exportacao_dados");
                entity.HasKey(x => x.LogExportacaoDadosId);
                entity.Property(x => x.FormatoArquivo).HasMaxLength(10);
                entity.Property(x => x.StatusOperacao).HasConversion<string>().HasMaxLength(20).IsRequired();
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.PerfilPaciente)
                    .WithMany()
                    .HasForeignKey(x => x.PacienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ParametrizacaoAlerta>(entity =>
            {
                entity.ToTable("parametrizacao_alerta");
                entity.HasKey(x => x.ParametrizacaoAlertaId);
                entity.Property(x => x.MetricaAlvo).HasMaxLength(50);
                entity.Property(x => x.ValorMinimoToleravel).HasPrecision(5, 2);
                entity.Property(x => x.ValorMaximoToleravel).HasPrecision(5, 2);
                entity.Property(x => x.MensagemCustomizada).HasMaxLength(255);
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HistoricoManutencaoDispositivo>(entity =>
            {
                entity.ToTable("historico_manutencao_dispositivo");
                entity.HasKey(x => x.HistoricoManutencaoDispositivoId);
                entity.Property(x => x.TipoIntervencao).HasConversion<string>().HasMaxLength(30).IsRequired();
                entity.Property(x => x.DescricaoDetalhada).HasColumnType("text");
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.Dispositivo)
                    .WithMany()
                    .HasForeignKey(x => x.DispositivoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LogInteracaoIA>(entity =>
            {
                entity.ToTable("log_interacao_ia");
                entity.HasKey(x => x.LogInteracaoIAId);
                entity.Property(x => x.PerguntaUsuario).HasColumnType("text");
                entity.Property(x => x.RespostaIa).HasColumnType("text");
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.PerfilPaciente)
                    .WithMany()
                    .HasForeignKey(x => x.ContextoPacienteId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<SessaoECGRawData>(entity =>
            {
                entity.ToTable("sessao_ecg_raw_data");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.ValoresSinalMv).HasColumnType("text");
                entity.HasOne(x => x.Login)
                    .WithMany()
                    .HasForeignKey(x => x.LoginId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(x => x.SessaoTelemetria)
                    .WithMany()
                    .HasForeignKey(x => x.SessaoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
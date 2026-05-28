using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuroVestAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "dispositivo",
                columns: table => new
                {
                    DispositivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoHardware = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeDispositivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StatusOperacional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BateriaPorcentagem = table.Column<int>(type: "int", nullable: false),
                    SinalPorcentagem = table.Column<int>(type: "int", nullable: false),
                    ModoFuncionamento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispositivo", x => x.DispositivoId);
                    table.ForeignKey(
                        name: "FK_dispositivo_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "parametrizacao_alerta",
                columns: table => new
                {
                    ParametrizacaoAlertaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetricaAlvo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValorMinimoToleravel = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ValorMaximoToleravel = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    MensagemCustomizada = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parametrizacao_alerta", x => x.ParametrizacaoAlertaId);
                    table.ForeignKey(
                        name: "FK_parametrizacao_alerta_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "perfil_medico",
                columns: table => new
                {
                    PerfilMedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Crm = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfil_medico", x => x.PerfilMedicoId);
                    table.ForeignKey(
                        name: "FK_perfil_medico_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "historico_manutencao_dispositivo",
                columns: table => new
                {
                    HistoricoManutencaoDispositivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DispositivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataManutencao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoIntervencao = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DescricaoDetalhada = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historico_manutencao_dispositivo", x => x.HistoricoManutencaoDispositivoId);
                    table.ForeignKey(
                        name: "FK_historico_manutencao_dispositivo_dispositivo_DispositivoId",
                        column: x => x.DispositivoId,
                        principalTable: "dispositivo",
                        principalColumn: "DispositivoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historico_manutencao_dispositivo_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId");
                });

            migrationBuilder.CreateTable(
                name: "perfil_paciente",
                columns: table => new
                {
                    PerfilPacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoResponsavelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CodigoPaciente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PesoKg = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    AlturaCm = table.Column<int>(type: "int", nullable: false),
                    Imc = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfil_paciente", x => x.PerfilPacienteId);
                    table.ForeignKey(
                        name: "FK_perfil_paciente_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_perfil_paciente_perfil_medico_MedicoResponsavelId",
                        column: x => x.MedicoResponsavelId,
                        principalTable: "perfil_medico",
                        principalColumn: "PerfilMedicoId");
                });

            migrationBuilder.CreateTable(
                name: "alerta_sistema",
                columns: table => new
                {
                    AlertaSistemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoSensor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Criticidade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alerta_sistema", x => x.AlertaSistemaId);
                    table.ForeignKey(
                        name: "FK_alerta_sistema_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_alerta_sistema_perfil_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "perfil_paciente",
                        principalColumn: "PerfilPacienteId");
                });

            migrationBuilder.CreateTable(
                name: "analise_ia_e_riscos",
                columns: table => new
                {
                    AnaliseIAeRiscosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoreGeral = table.Column<int>(type: "int", nullable: false),
                    RiscoVascular = table.Column<int>(type: "int", nullable: false),
                    RiscoCerebral = table.Column<int>(type: "int", nullable: false),
                    RiscoCardiaco = table.Column<int>(type: "int", nullable: false),
                    ClassificacaoTexto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analise_ia_e_riscos", x => x.AnaliseIAeRiscosId);
                    table.ForeignKey(
                        name: "FK_analise_ia_e_riscos_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_analise_ia_e_riscos_perfil_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "perfil_paciente",
                        principalColumn: "PerfilPacienteId");
                });

            migrationBuilder.CreateTable(
                name: "condicao_pre_existente",
                columns: table => new
                {
                    CondicaoPreExistenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCondicao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_condicao_pre_existente", x => x.CondicaoPreExistenteId);
                    table.ForeignKey(
                        name: "FK_condicao_pre_existente_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_condicao_pre_existente_perfil_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "perfil_paciente",
                        principalColumn: "PerfilPacienteId");
                });

            migrationBuilder.CreateTable(
                name: "log_exportacao_dados",
                columns: table => new
                {
                    LogExportacaoDadosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHoraSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FormatoArquivo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StatusOperacao = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_exportacao_dados", x => x.LogExportacaoDadosId);
                    table.ForeignKey(
                        name: "FK_log_exportacao_dados_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_log_exportacao_dados_perfil_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "perfil_paciente",
                        principalColumn: "PerfilPacienteId");
                });

            migrationBuilder.CreateTable(
                name: "log_interacao_ia",
                columns: table => new
                {
                    LogInteracaoIAId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContextoPacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PerguntaUsuario = table.Column<string>(type: "text", nullable: false),
                    RespostaIa = table.Column<string>(type: "text", nullable: false),
                    TokensConsumidos = table.Column<int>(type: "int", nullable: false),
                    DataHoraMensagem = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_log_interacao_ia", x => x.LogInteracaoIAId);
                    table.ForeignKey(
                        name: "FK_log_interacao_ia_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_log_interacao_ia_perfil_paciente_ContextoPacienteId",
                        column: x => x.ContextoPacienteId,
                        principalTable: "perfil_paciente",
                        principalColumn: "PerfilPacienteId");
                });

            migrationBuilder.CreateTable(
                name: "sessao_telemetria",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FcBpm = table.Column<int>(type: "int", nullable: false),
                    PaSistolica = table.Column<int>(type: "int", nullable: false),
                    PaDiastolica = table.Column<int>(type: "int", nullable: false),
                    Spo2 = table.Column<int>(type: "int", nullable: false),
                    VfcRmssd = table.Column<int>(type: "int", nullable: false),
                    AmplitudeEcg = table.Column<decimal>(type: "decimal(4,2)", precision: 4, scale: 2, nullable: false),
                    FluxoCerebral = table.Column<int>(type: "int", nullable: false),
                    OxigCerebralFnirs = table.Column<int>(type: "int", nullable: false),
                    IndiceAlfaBeta = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                    StatusGeral = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessao_telemetria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sessao_telemetria_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sessao_telemetria_perfil_paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "perfil_paciente",
                        principalColumn: "PerfilPacienteId");
                });

            migrationBuilder.CreateTable(
                name: "recomendacao_sistema",
                columns: table => new
                {
                    RecomendacaoSistemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnaliseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recomendacao_sistema", x => x.RecomendacaoSistemaId);
                    table.ForeignKey(
                        name: "FK_recomendacao_sistema_analise_ia_e_riscos_AnaliseId",
                        column: x => x.AnaliseId,
                        principalTable: "analise_ia_e_riscos",
                        principalColumn: "AnaliseIAeRiscosId");
                    table.ForeignKey(
                        name: "FK_recomendacao_sistema_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "metricas_ondas_eeg",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessaoId = table.Column<long>(type: "bigint", nullable: false),
                    Delta = table.Column<int>(type: "int", nullable: false),
                    Theta = table.Column<int>(type: "int", nullable: false),
                    Alpha = table.Column<int>(type: "int", nullable: false),
                    Beta = table.Column<int>(type: "int", nullable: false),
                    Gamma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metricas_ondas_eeg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_metricas_ondas_eeg_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_metricas_ondas_eeg_sessao_telemetria_SessaoId",
                        column: x => x.SessaoId,
                        principalTable: "sessao_telemetria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "sessao_ecg_raw_data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessaoId = table.Column<long>(type: "bigint", nullable: false),
                    FrequenciaAmostragemHz = table.Column<int>(type: "int", nullable: false),
                    ValoresSinalMv = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessao_ecg_raw_data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sessao_ecg_raw_data_login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "login",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sessao_ecg_raw_data_sessao_telemetria_SessaoId",
                        column: x => x.SessaoId,
                        principalTable: "sessao_telemetria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_alerta_sistema_LoginId",
                table: "alerta_sistema",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_alerta_sistema_PacienteId",
                table: "alerta_sistema",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_analise_ia_e_riscos_LoginId",
                table: "analise_ia_e_riscos",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_analise_ia_e_riscos_PacienteId",
                table: "analise_ia_e_riscos",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_condicao_pre_existente_LoginId",
                table: "condicao_pre_existente",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_condicao_pre_existente_PacienteId",
                table: "condicao_pre_existente",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_dispositivo_CodigoHardware",
                table: "dispositivo",
                column: "CodigoHardware",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dispositivo_LoginId",
                table: "dispositivo",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_historico_manutencao_dispositivo_DispositivoId",
                table: "historico_manutencao_dispositivo",
                column: "DispositivoId");

            migrationBuilder.CreateIndex(
                name: "IX_historico_manutencao_dispositivo_LoginId",
                table: "historico_manutencao_dispositivo",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_log_exportacao_dados_LoginId",
                table: "log_exportacao_dados",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_log_exportacao_dados_PacienteId",
                table: "log_exportacao_dados",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_log_interacao_ia_ContextoPacienteId",
                table: "log_interacao_ia",
                column: "ContextoPacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_log_interacao_ia_LoginId",
                table: "log_interacao_ia",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_login_Email",
                table: "login",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_metricas_ondas_eeg_LoginId",
                table: "metricas_ondas_eeg",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_metricas_ondas_eeg_SessaoId",
                table: "metricas_ondas_eeg",
                column: "SessaoId");

            migrationBuilder.CreateIndex(
                name: "IX_parametrizacao_alerta_LoginId",
                table: "parametrizacao_alerta",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_perfil_medico_Crm",
                table: "perfil_medico",
                column: "Crm",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_perfil_medico_LoginId",
                table: "perfil_medico",
                column: "LoginId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_perfil_paciente_CodigoPaciente",
                table: "perfil_paciente",
                column: "CodigoPaciente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_perfil_paciente_LoginId",
                table: "perfil_paciente",
                column: "LoginId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_perfil_paciente_MedicoResponsavelId",
                table: "perfil_paciente",
                column: "MedicoResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_recomendacao_sistema_AnaliseId",
                table: "recomendacao_sistema",
                column: "AnaliseId");

            migrationBuilder.CreateIndex(
                name: "IX_recomendacao_sistema_LoginId",
                table: "recomendacao_sistema",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_sessao_ecg_raw_data_LoginId",
                table: "sessao_ecg_raw_data",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_sessao_ecg_raw_data_SessaoId",
                table: "sessao_ecg_raw_data",
                column: "SessaoId");

            migrationBuilder.CreateIndex(
                name: "IX_sessao_telemetria_LoginId",
                table: "sessao_telemetria",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_sessao_telemetria_PacienteId",
                table: "sessao_telemetria",
                column: "PacienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alerta_sistema");

            migrationBuilder.DropTable(
                name: "condicao_pre_existente");

            migrationBuilder.DropTable(
                name: "historico_manutencao_dispositivo");

            migrationBuilder.DropTable(
                name: "log_exportacao_dados");

            migrationBuilder.DropTable(
                name: "log_interacao_ia");

            migrationBuilder.DropTable(
                name: "metricas_ondas_eeg");

            migrationBuilder.DropTable(
                name: "parametrizacao_alerta");

            migrationBuilder.DropTable(
                name: "recomendacao_sistema");

            migrationBuilder.DropTable(
                name: "sessao_ecg_raw_data");

            migrationBuilder.DropTable(
                name: "dispositivo");

            migrationBuilder.DropTable(
                name: "analise_ia_e_riscos");

            migrationBuilder.DropTable(
                name: "sessao_telemetria");

            migrationBuilder.DropTable(
                name: "perfil_paciente");

            migrationBuilder.DropTable(
                name: "perfil_medico");

            migrationBuilder.DropTable(
                name: "login");
        }
    }
}

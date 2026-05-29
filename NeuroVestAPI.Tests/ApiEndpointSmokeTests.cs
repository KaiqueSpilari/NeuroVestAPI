using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using NeuroVestAPI.Models;

namespace NeuroVestAPI.Tests;

public class ApiEndpointSmokeTests : IClassFixture<ApiTestFactory>
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly HttpClient _client;

    public ApiEndpointSmokeTests(ApiTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public Task LoginCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var tag = NewTag();

        await ExecuteCrudAsync<Login>(
            "/api/Login",
            new { email = $"login-{tag}@email.com", senhaHash = "Admin@123", tipoUsuario = "ADMIN" },
            login => login.LoginId.ToString(),
            login => login.Email = $"login-{tag}-updated@email.com",
            login => Assert.EndsWith("-updated@email.com", login.Email),
            cleanup);
    });

    [Fact]
    public Task PerfilMedicoCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var tag = NewTag();
        var login = await ExecuteCrudAsync<Login>(
            "/api/Login",
            new { email = $"medico-{tag}@email.com", senhaHash = "Medico@123", tipoUsuario = "MEDICO" },
            item => item.LoginId.ToString(),
            item => item.Email = $"medico-{tag}-updated@email.com",
            item => Assert.EndsWith("-updated@email.com", item.Email),
            cleanup);

        await ExecuteCrudAsync<PerfilMedico>(
            "/api/PerfilMedico",
            new { nomeCompleto = $"Dr. {tag}", crm = $"CRM{tag[..4]}", loginId = login.LoginId },
            item => item.PerfilMedicoId.ToString(),
            item => item.NomeCompleto = $"Dr. {tag} Atualizado",
            item => Assert.EndsWith("Atualizado", item.NomeCompleto),
            cleanup);
    });

    [Fact]
    public Task PerfilPacienteCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var tag = NewTag();
        var medicoLogin = await ExecuteCrudAsync<Login>(
            "/api/Login",
            new { email = $"medico-{tag}@email.com", senhaHash = "Medico@123", tipoUsuario = "MEDICO" },
            item => item.LoginId.ToString(),
            item => item.Email = $"medico-{tag}-updated@email.com",
            item => Assert.EndsWith("-updated@email.com", item.Email),
            cleanup);

        var perfilMedico = await ExecuteCrudAsync<PerfilMedico>(
            "/api/PerfilMedico",
            new { nomeCompleto = $"Dr. {tag}", crm = $"CRM{tag[..4]}", loginId = medicoLogin.LoginId },
            item => item.PerfilMedicoId.ToString(),
            item => item.NomeCompleto = $"Dr. {tag} Atualizado",
            item => Assert.EndsWith("Atualizado", item.NomeCompleto),
            cleanup);

        var pacienteLogin = await ExecuteCrudAsync<Login>(
            "/api/Login",
            new { email = $"paciente-{tag}@email.com", senhaHash = "Paciente@123", tipoUsuario = "PACIENTE" },
            item => item.LoginId.ToString(),
            item => item.SenhaHash = "Paciente@1234",
            item => Assert.Equal("Paciente@1234", item.SenhaHash),
            cleanup);

        await ExecuteCrudAsync<PerfilPaciente>(
            "/api/PerfilPaciente",
            new
            {
                codigoPaciente = $"PAC-{tag}",
                nomeCompleto = $"Paciente {tag}",
                idade = 42,
                sexo = "MASCULINO",
                pesoKg = 82.5m,
                alturaCm = 178,
                imc = 26.0m,
                loginId = pacienteLogin.LoginId,
                medicoResponsavelId = perfilMedico.PerfilMedicoId
            },
            item => item.PerfilPacienteId.ToString(),
            item => item.NomeCompleto = $"Paciente {tag} Atualizado",
            item => Assert.EndsWith("Atualizado", item.NomeCompleto),
            cleanup);
    });

    [Fact]
    public Task CondicaoPreExistenteCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var seed = await SeedPacienteAsync(cleanup);

        await ExecuteCrudAsync<CondicaoPreExistente>(
            "/api/CondicaoPreExistente",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                pacienteId = seed.PerfilPaciente.PerfilPacienteId,
                nomeCondicao = $"Hipertensao {seed.Tag}"
            },
            item => item.CondicaoPreExistenteId.ToString(),
            item => item.NomeCondicao = $"Hipertensao {seed.Tag} controlada",
            item => Assert.Contains("controlada", item.NomeCondicao),
            cleanup);
    });

    [Fact]
    public Task DispositivoCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var adminLogin = await ExecuteCrudAsync<Login>(
            "/api/Login",
            new { email = $"admin-{NewTag()}@email.com", senhaHash = "Admin@123", tipoUsuario = "ADMIN" },
            item => item.LoginId.ToString(),
            item => item.Email = item.Email.Replace("@email.com", ".updated@email.com"),
            item => Assert.Contains("updated", item.Email),
            cleanup);

        var tag = NewTag();
        await ExecuteCrudAsync<Dispositivo>(
            "/api/Dispositivo",
            new
            {
                loginId = adminLogin.LoginId,
                codigoHardware = $"HW-{tag}",
                nomeDispositivo = $"Dispositivo {tag}",
                tipo = TipoDispositivo.VASCULAR,
                statusOperacional = "OK",
                bateriaPorcentagem = 88,
                sinalPorcentagem = 91,
                modoFuncionamento = ModoFuncionamento.SIMULACAO
            },
            item => item.DispositivoId.ToString(),
            item => item.StatusOperacional = "MANUTENCAO",
            item => Assert.Equal("MANUTENCAO", item.StatusOperacional),
            cleanup);
    });

    [Fact]
    public Task SessaoTelemetriaCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var seed = await SeedPacienteAsync(cleanup);

        await ExecuteCrudAsync<SessaoTelemetria>(
            "/api/SessaoTelemetria",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                pacienteId = seed.PerfilPaciente.PerfilPacienteId,
                dataHora = DateTime.UtcNow,
                fcBpm = 72,
                paSistolica = 120,
                paDiastolica = 80,
                spo2 = 98,
                vfcRmssd = 32,
                amplitudeEcg = 1.25m,
                fluxoCerebral = 77,
                oxigCerebralFnirs = 89,
                indiceAlfaBeta = 2.1m,
                statusGeral = StatusGeral.NORMAL
            },
            item => item.Id.ToString(),
            item => item.FcBpm = 75,
            item => Assert.Equal(75, item.FcBpm),
            cleanup);
    });

    [Fact]
    public Task MetricasOndasEEGCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var seed = await SeedPatientSessionAsync(cleanup);

        await ExecuteCrudAsync<MetricasOndasEEG>(
            "/api/MetricasOndasEEG",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                sessaoId = seed.SessaoTelemetria.Id,
                delta = 10,
                theta = 11,
                alpha = 12,
                beta = 13,
                gamma = 14
            },
            item => item.Id.ToString(),
            item => item.Alpha = 22,
            item => Assert.Equal(22, item.Alpha),
            cleanup);
    });

    [Fact]
    public Task AnaliseIAeRiscosCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var seed = await SeedPacienteAsync(cleanup);

        await ExecuteCrudAsync<AnaliseIAeRiscos>(
            "/api/AnaliseIAeRiscos",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                pacienteId = seed.PerfilPaciente.PerfilPacienteId,
                scoreGeral = 70,
                riscoVascular = 60,
                riscoCerebral = 50,
                riscoCardiaco = 40,
                classificacaoTexto = "Moderado"
            },
            item => item.AnaliseIAeRiscosId.ToString(),
            item => item.ClassificacaoTexto = "Moderado revisado",
            item => Assert.EndsWith("revisado", item.ClassificacaoTexto),
            cleanup);
    });

    [Fact]
    public Task RecomendacaoSistemaCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var seed = await SeedAnalysisGraphAsync(cleanup);

        await ExecuteCrudAsync<RecomendacaoSistema>(
            "/api/RecomendacaoSistema",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                analiseId = seed.AnaliseIAeRiscos.AnaliseIAeRiscosId,
                categoria = CategoriaRecomendacao.PREVENTIVO,
                descricao = "Acompanhar rotina"
            },
            item => item.RecomendacaoSistemaId.ToString(),
            item => item.Descricao = "Acompanhar rotina revisada",
            item => Assert.EndsWith("revisada", item.Descricao),
            cleanup);
    });

    [Fact]
    public Task AlertaSistemaCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var seed = await SeedPacienteAsync(cleanup);

        await ExecuteCrudAsync<AlertaSistema>(
            "/api/AlertaSistema",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                pacienteId = seed.PerfilPaciente.PerfilPacienteId,
                dataHora = DateTime.UtcNow,
                tipoSensor = "ECG",
                mensagem = "Alerta gerado",
                criticidade = CriticidadeAlerta.ATENCAO
            },
            item => item.AlertaSistemaId.ToString(),
            item => item.Mensagem = "Alerta gerado atualizado",
            item => Assert.EndsWith("atualizado", item.Mensagem),
            cleanup);
    });

    [Fact]
    public Task LogExportacaoDadosCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var seed = await SeedPacienteAsync(cleanup);

        await ExecuteCrudAsync<LogExportacaoDados>(
            "/api/LogExportacaoDados",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                pacienteId = seed.PerfilPaciente.PerfilPacienteId,
                dataHoraSolicitacao = DateTime.UtcNow,
                formatoArquivo = "CSV",
                statusOperacao = StatusExportacao.SUCESSO
            },
            item => item.LogExportacaoDadosId.ToString(),
            item => item.FormatoArquivo = "JSON",
            item => Assert.Equal("JSON", item.FormatoArquivo),
            cleanup);
    });

    [Fact]
    public Task ParametrizacaoAlertaCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var adminLogin = await ExecuteCrudAsync<Login>(
            "/api/Login",
            new { email = $"param-{NewTag()}@email.com", senhaHash = "Admin@123", tipoUsuario = "ADMIN" },
            item => item.LoginId.ToString(),
            item => item.SenhaHash = "Admin@1234",
            item => Assert.Equal("Admin@1234", item.SenhaHash),
            cleanup);

        await ExecuteCrudAsync<ParametrizacaoAlerta>(
            "/api/ParametrizacaoAlerta",
            new
            {
                loginId = adminLogin.LoginId,
                metricaAlvo = "FrequenciaCardiaca",
                valorMinimoToleravel = 60m,
                valorMaximoToleravel = 120m,
                mensagemCustomizada = "Monitorar FC"
            },
            item => item.ParametrizacaoAlertaId.ToString(),
            item => item.MensagemCustomizada = "Monitorar FC ajustado",
            item => Assert.EndsWith("ajustado", item.MensagemCustomizada),
            cleanup);
    });

    [Fact]
    public Task HistoricoManutencaoDispositivoCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var adminLogin = await ExecuteCrudAsync<Login>(
            "/api/Login",
            new { email = $"hist-{NewTag()}@email.com", senhaHash = "Admin@123", tipoUsuario = "ADMIN" },
            item => item.LoginId.ToString(),
            item => item.Email = item.Email.Replace("@email.com", ".updated@email.com"),
            item => Assert.Contains("updated", item.Email),
            cleanup);

        var dispositivo = await ExecuteCrudAsync<Dispositivo>(
            "/api/Dispositivo",
            new
            {
                loginId = adminLogin.LoginId,
                codigoHardware = $"HW-{NewTag()}",
                nomeDispositivo = "Dispositivo Historico",
                tipo = TipoDispositivo.VASCULAR,
                statusOperacional = "OK",
                bateriaPorcentagem = 88,
                sinalPorcentagem = 91,
                modoFuncionamento = ModoFuncionamento.SIMULACAO
            },
            item => item.DispositivoId.ToString(),
            item => item.StatusOperacional = "MANUTENCAO",
            item => Assert.Equal("MANUTENCAO", item.StatusOperacional),
            cleanup);

        await ExecuteCrudAsync<HistoricoManutencaoDispositivo>(
            "/api/HistoricoManutencaoDispositivo",
            new
            {
                loginId = adminLogin.LoginId,
                dispositivoId = dispositivo.DispositivoId,
                dataManutencao = DateTime.UtcNow,
                tipoIntervencao = TipoIntervencao.RECALIBRACAO,
                descricaoDetalhada = "Ajuste inicial"
            },
            item => item.HistoricoManutencaoDispositivoId.ToString(),
            item => item.DescricaoDetalhada = "Ajuste inicial revisado",
            item => Assert.EndsWith("revisado", item.DescricaoDetalhada),
            cleanup);
    });

    [Fact]
    public Task LogInteracaoIACrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var seed = await SeedPacienteAsync(cleanup);

        await ExecuteCrudAsync<LogInteracaoIA>(
            "/api/LogInteracaoIA",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                contextoPacienteId = seed.PerfilPaciente.PerfilPacienteId,
                perguntaUsuario = "Como estou?",
                respostaIa = "Em monitoramento",
                tokensConsumidos = 120,
                dataHoraMensagem = DateTime.UtcNow
            },
            item => item.LogInteracaoIAId.ToString(),
            item => item.TokensConsumidos = 150,
            item => Assert.Equal(150, item.TokensConsumidos),
            cleanup);
    });

    [Fact]
    public Task SessaoECGRawDataCrud_Works() => RunWithCleanupAsync(async cleanup =>
    {
        var seed = await SeedPatientSessionAsync(cleanup);

        await ExecuteCrudAsync<SessaoECGRawData>(
            "/api/SessaoECGRawData",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                sessaoId = seed.SessaoTelemetria.Id,
                frequenciaAmostragemHz = 250,
                valoresSinalMv = "[0.12,0.15,0.10]"
            },
            item => item.Id.ToString(),
            item => item.FrequenciaAmostragemHz = 300,
            item => Assert.Equal(300, item.FrequenciaAmostragemHz),
            cleanup);
    });

    [Fact]
    public Task CollectionEndpoints_ReturnOk() => RunWithCleanupAsync(async cleanup =>
    {
        var routes = new[]
        {
            "/api/Login",
            "/api/PerfilMedico",
            "/api/PerfilPaciente",
            "/api/CondicaoPreExistente",
            "/api/Dispositivo",
            "/api/SessaoTelemetria",
            "/api/MetricasOndasEEG",
            "/api/AnaliseIAeRiscos",
            "/api/RecomendacaoSistema",
            "/api/AlertaSistema",
            "/api/LogExportacaoDados",
            "/api/ParametrizacaoAlerta",
            "/api/HistoricoManutencaoDispositivo",
            "/api/LogInteracaoIA",
            "/api/SessaoECGRawData"
        };

        foreach (var route in routes)
        {
            var listResponse = await _client.GetAsync(route);
            Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
        }
    });

    private async Task RunWithCleanupAsync(Func<List<(string Route, string Id)>, Task> body)
    {
        var cleanup = new List<(string Route, string Id)>();

        try
        {
            await body(cleanup);
        }
        finally
        {
            await CleanupAsync(cleanup);
        }
    }

    private async Task<T> ExecuteCrudAsync<T>(
        string route,
        object createBody,
        Func<T, string> idSelector,
        Action<T> mutate,
        Action<T> verifyUpdated,
        ICollection<(string Route, string Id)> deletions)
    {
        var created = await PostAsync<T>(route, createBody);
        var id = idSelector(created);
        Assert.False(string.IsNullOrWhiteSpace(id));

        deletions.Add((route, id));

        var getResponse = await _client.GetAsync($"{route}/{id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        mutate(created);

        var putResponse = await _client.PutAsJsonAsync($"{route}/{id}", created, JsonOptions);
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        var updated = await GetAsync<T>($"{route}/{id}");
        verifyUpdated(updated);

        return updated;
    }

    private async Task<Login> SeedLoginAsync(string emailPrefix, string tipoUsuario, ICollection<(string Route, string Id)> deletions)
    {
        return await ExecuteCrudAsync<Login>(
            "/api/Login",
            new { email = $"{emailPrefix}@email.com", senhaHash = "Admin@123", tipoUsuario },
            item => item.LoginId.ToString(),
            item => item.Email = $"{emailPrefix}-updated@email.com",
            item => Assert.Contains("updated", item.Email),
            deletions);
    }

    private async Task<SeededPacienteGraph> SeedPacienteAsync(ICollection<(string Route, string Id)> deletions)
    {
        var tag = NewTag();
        var adminLogin = await SeedLoginAsync($"admin-{tag}", "ADMIN", deletions);
        var medicoLogin = await SeedLoginAsync($"medico-{tag}", "MEDICO", deletions);
        var pacienteLogin = await SeedLoginAsync($"paciente-{tag}", "PACIENTE", deletions);

        var perfilMedico = await ExecuteCrudAsync<PerfilMedico>(
            "/api/PerfilMedico",
            new { nomeCompleto = $"Dr. {tag}", crm = $"CRM{tag[..4]}", loginId = medicoLogin.LoginId },
            item => item.PerfilMedicoId.ToString(),
            item => item.NomeCompleto = $"Dr. {tag} Atualizado",
            item => Assert.EndsWith("Atualizado", item.NomeCompleto),
            deletions);

        var perfilPaciente = await ExecuteCrudAsync<PerfilPaciente>(
            "/api/PerfilPaciente",
            new
            {
                codigoPaciente = $"PAC-{tag}",
                nomeCompleto = $"Paciente {tag}",
                idade = 42,
                sexo = "MASCULINO",
                pesoKg = 82.5m,
                alturaCm = 178,
                imc = 26.0m,
                loginId = pacienteLogin.LoginId,
                medicoResponsavelId = perfilMedico.PerfilMedicoId
            },
            item => item.PerfilPacienteId.ToString(),
            item => item.NomeCompleto = $"Paciente {tag} Atualizado",
            item => Assert.EndsWith("Atualizado", item.NomeCompleto),
            deletions);

        return new SeededPacienteGraph(tag, adminLogin, medicoLogin, pacienteLogin, perfilMedico, perfilPaciente);
    }

    private async Task<SeededSessionGraph> SeedPatientSessionAsync(ICollection<(string Route, string Id)> deletions)
    {
        var seed = await SeedPacienteAsync(deletions);

        var dispositivo = await ExecuteCrudAsync<Dispositivo>(
            "/api/Dispositivo",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                codigoHardware = $"HW-{seed.Tag}",
                nomeDispositivo = $"Dispositivo {seed.Tag}",
                tipo = TipoDispositivo.VASCULAR,
                statusOperacional = "OK",
                bateriaPorcentagem = 88,
                sinalPorcentagem = 91,
                modoFuncionamento = ModoFuncionamento.SIMULACAO
            },
            item => item.DispositivoId.ToString(),
            item => item.StatusOperacional = "MANUTENCAO",
            item => Assert.Equal("MANUTENCAO", item.StatusOperacional),
            deletions);

        var sessaoTelemetria = await ExecuteCrudAsync<SessaoTelemetria>(
            "/api/SessaoTelemetria",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                pacienteId = seed.PerfilPaciente.PerfilPacienteId,
                dataHora = DateTime.UtcNow,
                fcBpm = 72,
                paSistolica = 120,
                paDiastolica = 80,
                spo2 = 98,
                vfcRmssd = 32,
                amplitudeEcg = 1.25m,
                fluxoCerebral = 77,
                oxigCerebralFnirs = 89,
                indiceAlfaBeta = 2.1m,
                statusGeral = StatusGeral.NORMAL
            },
            item => item.Id.ToString(),
            item => item.FcBpm = 75,
            item => Assert.Equal(75, item.FcBpm),
            deletions);

        return new SeededSessionGraph(seed.Tag, seed.AdminLogin, seed.MedicoLogin, seed.PacienteLogin, seed.PerfilMedico, seed.PerfilPaciente, dispositivo, sessaoTelemetria);
    }

    private async Task<SeededAnalysisGraph> SeedAnalysisGraphAsync(ICollection<(string Route, string Id)> deletions)
    {
        var seed = await SeedPacienteAsync(deletions);

        var analise = await ExecuteCrudAsync<AnaliseIAeRiscos>(
            "/api/AnaliseIAeRiscos",
            new
            {
                loginId = seed.AdminLogin.LoginId,
                pacienteId = seed.PerfilPaciente.PerfilPacienteId,
                scoreGeral = 70,
                riscoVascular = 60,
                riscoCerebral = 50,
                riscoCardiaco = 40,
                classificacaoTexto = "Moderado"
            },
            item => item.AnaliseIAeRiscosId.ToString(),
            item => item.ClassificacaoTexto = "Moderado revisado",
            item => Assert.EndsWith("revisado", item.ClassificacaoTexto),
            deletions);

        return new SeededAnalysisGraph(seed.Tag, seed.AdminLogin, seed.MedicoLogin, seed.PacienteLogin, seed.PerfilMedico, seed.PerfilPaciente, analise);
    }

    private async Task<T> PostAsync<T>(string route, object body)
    {
        var response = await _client.PostAsJsonAsync(route, body, JsonOptions);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        return await ReadAsync<T>(response);
    }

    private async Task<T> GetAsync<T>(string route)
    {
        var response = await _client.GetAsync(route);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return await ReadAsync<T>(response);
    }

    private static async Task<T> ReadAsync<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, JsonOptions)!;
    }

    private async Task CleanupAsync(IEnumerable<(string Route, string Id)> deletions)
    {
        foreach (var deletion in deletions.Reverse())
        {
            var deleteResponse = await _client.DeleteAsync($"{deletion.Route}/{deletion.Id}");
            if (deleteResponse.StatusCode != HttpStatusCode.NoContent && deleteResponse.StatusCode != HttpStatusCode.NotFound)
            {
                Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            }
        }
    }

    private static string NewTag() => Guid.NewGuid().ToString("N")[..8];

    private sealed record SeededPacienteGraph(
        string Tag,
        Login AdminLogin,
        Login MedicoLogin,
        Login PacienteLogin,
        PerfilMedico PerfilMedico,
        PerfilPaciente PerfilPaciente);

    private sealed record SeededSessionGraph(
        string Tag,
        Login AdminLogin,
        Login MedicoLogin,
        Login PacienteLogin,
        PerfilMedico PerfilMedico,
        PerfilPaciente PerfilPaciente,
        Dispositivo Dispositivo,
        SessaoTelemetria SessaoTelemetria);

    private sealed record SeededAnalysisGraph(
        string Tag,
        Login AdminLogin,
        Login MedicoLogin,
        Login PacienteLogin,
        PerfilMedico PerfilMedico,
        PerfilPaciente PerfilPaciente,
        AnaliseIAeRiscos AnaliseIAeRiscos);
}
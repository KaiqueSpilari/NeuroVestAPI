# Passo a passo: controllers, migrations, testes e populacao do banco

## Objetivo

Este documento resume o fluxo completo que foi feito no projeto NeuroVestAPI: criacao do contexto do Entity Framework, scaffolding das controllers, criacao e aplicacao das migrations, criacao dos testes de integracao e a carga manual dos endpoints para gravar dados no banco real.

## 1. Criar o contexto do banco

O projeto passou a usar um `DbContext` central em `Models/AppDbContext.cs`, com os `DbSet` das entidades do dominio e o mapeamento das relacoes.

Pontos principais:
- O contexto ficou ligado ao SQL Server.
- As relacoes com multiplos caminhos de cascata foram ajustadas para `DeleteBehavior.NoAction` quando necessario.
- O `Program.cs` foi configurado para registrar o contexto com `AddDbContext<AppDbContext>()`.

Arquivos envolvidos:
- `Program.cs`
- `Models/AppDbContext.cs`

## 2. Ignorar arquivos gerados

Foi criado o `.gitignore` para nao subir artefatos gerados e a pasta de dados local.

Entradas principais:
- `bin/`
- `obj/`
- `Data/`

## 3. Gerar as controllers

As controllers foram criadas com scaffold para expor os endpoints CRUD de cada entidade.

Controllers existentes:
- `Controllers/LoginController.cs`
- `Controllers/PerfilMedicoController.cs`
- `Controllers/PerfilPacienteController.cs`
- `Controllers/CondicaoPreExistenteController.cs`
- `Controllers/DispositivoController.cs`
- `Controllers/SessaoTelemetriaController.cs`
- `Controllers/MetricasOndasEEGController.cs`
- `Controllers/AnaliseIAeRiscosController.cs`
- `Controllers/RecomendacaoSistemaController.cs`
- `Controllers/AlertaSistemaController.cs`
- `Controllers/LogExportacaoDadosController.cs`
- `Controllers/ParametrizacaoAlertaController.cs`
- `Controllers/HistoricoManutencaoDispositivoController.cs`
- `Controllers/LogInteracaoIAController.cs`
- `Controllers/SessaoECGRawDataController.cs`

Observacao importante:
- O endpoint de `Login` precisou de ajuste para receber um DTO proprio, `Models/LoginCreateRequest.cs`, para nao depender do objeto inteiro no POST.

## 4. Criar a migration inicial

Depois do contexto estar pronto, foi criada a migration inicial para representar o modelo no banco.

Fluxo executado:
- `dotnet ef migrations add InitialCreate -c AppDbContext`
- `dotnet ef database update -c AppDbContext`

A migration foi ajustada para respeitar o SQL Server e evitar conflitos de cascata.

Arquivos gerados:
- `Migrations/20260528174237_InitialCreate.cs`
- `Migrations/AppDbContextModelSnapshot.cs`

## 5. Ajustar o contrato do Login

O POST de login passou a aceitar um payload menor e mais claro.

Campos usados no POST de login:
- `email`
- `senhaHash`
- `tipoUsuario`
- `dataCriacao` opcional

Exemplo:
```json
{
  "email": "admin@email.com",
  "senhaHash": "Admin@123",
  "tipoUsuario": "ADMIN"
}
```

## 6. Criar os testes de integracao

Foi criado um projeto de testes separado em `NeuroVestAPI.Tests` para rodar chamadas HTTP reais contra o host, sem depender do SQL Server real.

Arquivos principais:
- `NeuroVestAPI.Tests/ApiTestFactory.cs`
- `NeuroVestAPI.Tests/ApiEndpointSmokeTests.cs`

Como funciona:
- O `WebApplicationFactory<Program>` sobe a API em ambiente de teste.
- O banco do teste usa SQLite in-memory.
- O contexto original do SQL Server e as configuracoes conflitantes sao removidos no host de teste.
- O schema e criado automaticamente no inicio dos testes.

Isso permitiu validar os endpoints sem mexer no banco real.

## 7. Escrever testes individuais para cada endpoint

Os testes foram organizados em casos individuais, um para cada area da API.

Cobertura final validada:
- `LoginCrud_Works`
- `PerfilMedicoCrud_Works`
- `PerfilPacienteCrud_Works`
- `CondicaoPreExistenteCrud_Works`
- `DispositivoCrud_Works`
- `SessaoTelemetriaCrud_Works`
- `MetricasOndasEEGCrud_Works`
- `AnaliseIAeRiscosCrud_Works`
- `RecomendacaoSistemaCrud_Works`
- `AlertaSistemaCrud_Works`
- `LogExportacaoDadosCrud_Works`
- `ParametrizacaoAlertaCrud_Works`
- `HistoricoManutencaoDispositivoCrud_Works`
- `LogInteracaoIACrud_Works`
- `SessaoECGRawDataCrud_Works`
- `CollectionEndpoints_ReturnOk`

Resultado final dos testes:
- 16 testes executados com sucesso.

## 8. Rodar a API localmente

A API foi executada com:
- `dotnet run`

Ela ficou disponivel em:
- `http://localhost:5274`

## 9. Fazer a populacao manual do banco real

Depois da validacao com testes, foi feito um teste manual contra a API real para gravar dados no banco SQL Server configurado no projeto.

Ordem usada na carga:
1. Criar tres logins:
   - admin
   - medico
   - paciente
2. Criar `PerfilMedico`
3. Criar `PerfilPaciente`
4. Criar `Dispositivo`
5. Criar `SessaoTelemetria`
6. Criar `MetricasOndasEEG`
7. Criar `AnaliseIAeRiscos`
8. Criar `RecomendacaoSistema`
9. Criar `AlertaSistema`
10. Criar `LogExportacaoDados`
11. Criar `ParametrizacaoAlerta`
12. Criar `HistoricoManutencaoDispositivo`
13. Criar `LogInteracaoIA`
14. Criar `SessaoECGRawData`
15. Criar `CondicaoPreExistente`

Pontos importantes da carga manual:
- Os IDs GUID foram enviados explicitamente nas entidades que usam chave `Guid`.
- As entidades com chave `long` deixaram o banco gerar o valor automaticamente.
- As dependencias de chave estrangeira foram respeitadas na ordem de criacao.
- Os enums foram enviados pelo valor numerico esperado pelo model binder.

## 10. Valores de enums usados na carga

### TipoUsuario
- `0` = `MEDICO`
- `1` = `PACIENTE`
- `2` = `ADMIN`

### TipoDispositivo
- `0` = `VASCULAR`
- `1` = `NEURAL`

### ModoFuncionamento
- `0` = `SIMULACAO`
- `1` = `REAL`

### StatusGeral
- `0` = `NORMAL`
- `1` = `ATENCAO`
- `2` = `CRITICO`

### CategoriaRecomendacao
- `0` = `URGENTE`
- `1` = `IMPORTANTE`
- `2` = `PREVENTIVO`
- `3` = `ESTILO_DE_VIDA`

### CriticidadeAlerta
- `0` = `INFO`
- `1` = `ATENCAO`
- `2` = `URGENTE`

### StatusExportacao
- `0` = `SUCESSO`
- `1` = `FALHA`

### TipoIntervencao
- `0` = `RECALIBRACAO`
- `1` = `TROCA_BATERIA`
- `2` = `ATUALIZACAO_FIRMWARE`

## 11. Como validar que os dados foram gravados

A verificacao final foi feita com chamadas `GET` nas colecoes da API.

Resultado final obtido:
- `Login: 5`
- `PerfilMedico: 1`
- `PerfilPaciente: 1`
- `CondicaoPreExistente: 1`
- `Dispositivo: 1`
- `SessaoTelemetria: 1`
- `MetricasOndasEEG: 1`
- `AnaliseIAeRiscos: 1`
- `RecomendacaoSistema: 1`
- `AlertaSistema: 1`
- `LogExportacaoDados: 1`
- `ParametrizacaoAlerta: 1`
- `HistoricoManutencaoDispositivo: 1`
- `LogInteracaoIA: 1`
- `SessaoECGRawData: 1`

## 12. Resumo final do fluxo

O fluxo completo ficou assim:
- Criar o contexto do EF Core.
- Registrar o contexto no `Program.cs`.
- Criar os controllers scaffoldados.
- Ajustar o contrato do `Login` com DTO proprio.
- Criar a migration e aplicar no banco.
- Criar testes de integracao com SQLite in-memory.
- Rodar os testes individuais de cada endpoint.
- Subir a API local.
- Fazer a carga manual no banco real por HTTP.
- Confirmar que os dados ficaram gravados em todas as tabelas principais.

Se quiser, o proximo passo pode ser transformar este documento em uma versao mais curta, em formato de checklist, ou em um guia tecnico para outra pessoa repetir o processo do zero.

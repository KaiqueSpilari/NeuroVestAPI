# Documentação completa do projeto NeuroVestAPI

## Visão geral

O NeuroVestAPI é uma API ASP.NET Core Web API em .NET 10 para o domínio de monitoramento neurocardiovascular. O projeto expõe endpoints CRUD para várias entidades clínicas e operacionais, usa Entity Framework Core com SQL Server no ambiente real e conta com uma suíte de testes de integração com SQLite em memória.

O fluxo que foi implementado e validado neste repositório incluiu:

- Criação do contexto do banco.
- Configuração do `Program.cs`.
- Geração das controllers via scaffold.
- Criação e aplicação da migration inicial.
- Criação de testes de integração isolados.
- Testes manuais contra a API real.
- População do banco com dados reais.
- Documentação do processo.

## Tecnologias usadas

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server no ambiente real
- SQLite in-memory nos testes
- Swagger/OpenAPI e Scalar para exploração da API
- xUnit para testes de integração

## Estrutura principal do projeto

### Arquivos centrais

- `Program.cs`: inicialização da aplicação, registro do DbContext e mapeamento dos endpoints.
- `Data/AppDbContext.cs`: contexto do Entity Framework e mapeamento das entidades.
- `appsettings.json`: string de conexão com o SQL Server.
- `Migrations/`: migration inicial e snapshot do modelo.
- `Controllers/`: controllers CRUD de cada entidade.
- `Models/`: entidades do domínio e DTO de criação do login.
- `NeuroVestAPI.Tests/`: projeto de testes de integração.

### Observação sobre o .gitignore

O arquivo `.gitignore` atual ignora `bin/` e `obj/`.

## Inicialização da API

O `Program.cs` registra os serviços principais da aplicação:

- `AddControllers()` com `SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true`.
- `AddDbContext<AppDbContext>()` com `UseSqlServer()`.
- `AddOpenApi()` para documentação de API.
- `MapOpenApi()` e `MapScalarApiReference()` apenas em Development.
- `MapControllers()` para publicar os endpoints.
- `MapGet("/", ...)` como rota simples de saúde.

A aplicação também expõe `public partial class Program { }` para permitir testes com `WebApplicationFactory`.

## Banco de dados e contexto

O contexto está em `Data/AppDbContext.cs` e centraliza os `DbSet` do domínio:

- `Login`
- `PerfilMedico`
- `PerfilPaciente`
- `CondicaoPreExistente`
- `Dispositivo`
- `SessaoTelemetria`
- `MetricasOndasEEG`
- `AnaliseIAeRiscos`
- `RecomendacaoSistema`
- `AlertaSistema`
- `LogExportacaoDados`
- `ParametrizacaoAlerta`
- `HistoricoManutencaoDispositivo`
- `LogInteracaoIA`
- `SessaoECGRawData`

### Pontos de modelagem importantes

- A tabela `login` usa `LoginId` como chave principal.
- O campo `TipoUsuario` é salvo como string no banco.
- `PerfilMedico` e `PerfilPaciente` têm relacionamento 1:1 com `Login`.
- Algumas relações usam `DeleteBehavior.NoAction` para evitar múltiplos caminhos de cascade no SQL Server.
- Campos monetários ou numéricos sensíveis usam precisão definida no modelo.
- Alguns campos de texto usam tamanho máximo ou `text`.

## Entidades do domínio

### Login

Campos principais:

- `LoginId`
- `Email`
- `SenhaHash`
- `TipoUsuario`
- `DataCriacao`

### PerfilMedico

Campos principais:

- `PerfilMedicoId`
- `NomeCompleto`
- `Crm`
- `LoginId`

### PerfilPaciente

Campos principais:

- `PerfilPacienteId`
- `LoginId`
- `MedicoResponsavelId`
- `CodigoPaciente`
- `NomeCompleto`
- `Idade`
- `Sexo`
- `PesoKg`
- `AlturaCm`
- `Imc`

### CondicaoPreExistente

Campos principais:

- `CondicaoPreExistenteId`
- `LoginId`
- `PacienteId`
- `NomeCondicao`

### Dispositivo

Campos principais:

- `DispositivoId`
- `LoginId`
- `CodigoHardware`
- `NomeDispositivo`
- `Tipo`
- `StatusOperacional`
- `BateriaPorcentagem`
- `SinalPorcentagem`
- `ModoFuncionamento`

### SessaoTelemetria

Campos principais:

- `Id`
- `LoginId`
- `PacienteId`
- `DataHora`
- `FcBpm`
- `PaSistolica`
- `PaDiastolica`
- `Spo2`
- `VfcRmssd`
- `AmplitudeEcg`
- `FluxoCerebral`
- `OxigCerebralFnirs`
- `IndiceAlfaBeta`
- `StatusGeral`

### MetricasOndasEEG

Campos principais:

- `Id`
- `LoginId`
- `SessaoId`
- `Delta`
- `Theta`
- `Alpha`
- `Beta`
- `Gamma`

### AnaliseIAeRiscos

Campos principais:

- `AnaliseIAeRiscosId`
- `LoginId`
- `PacienteId`
- `ScoreGeral`
- `RiscoVascular`
- `RiscoCerebral`
- `RiscoCardiaco`
- `ClassificacaoTexto`

### RecomendacaoSistema

Campos principais:

- `RecomendacaoSistemaId`
- `LoginId`
- `AnaliseId`
- `Categoria`
- `Descricao`

### AlertaSistema

Campos principais:

- `AlertaSistemaId`
- `LoginId`
- `PacienteId`
- `DataHora`
- `TipoSensor`
- `Mensagem`
- `Criticidade`

### LogExportacaoDados

Campos principais:

- `LogExportacaoDadosId`
- `LoginId`
- `PacienteId`
- `DataHoraSolicitacao`
- `FormatoArquivo`
- `StatusOperacao`

### ParametrizacaoAlerta

Campos principais:

- `ParametrizacaoAlertaId`
- `LoginId`
- `MetricaAlvo`
- `ValorMinimoToleravel`
- `ValorMaximoToleravel`
- `MensagemCustomizada`

### HistoricoManutencaoDispositivo

Campos principais:

- `HistoricoManutencaoDispositivoId`
- `LoginId`
- `DispositivoId`
- `DataManutencao`
- `TipoIntervencao`
- `DescricaoDetalhada`

### LogInteracaoIA

Campos principais:

- `LogInteracaoIAId`
- `LoginId`
- `ContextoPacienteId`
- `PerguntaUsuario`
- `RespostaIa`
- `TokensConsumidos`
- `DataHoraMensagem`

### SessaoECGRawData

Campos principais:

- `Id`
- `LoginId`
- `SessaoId`
- `FrequenciaAmostragemHz`
- `ValoresSinalMv`

## Controllers expostas

As controllers publicadas seguem o padrão CRUD:

- `LoginController`
- `PerfilMedicoController`
- `PerfilPacienteController`
- `CondicaoPreExistenteController`
- `DispositivoController`
- `SessaoTelemetriaController`
- `MetricasOndasEEGController`
- `AnaliseIAeRiscosController`
- `RecomendacaoSistemaController`
- `AlertaSistemaController`
- `LogExportacaoDadosController`
- `ParametrizacaoAlertaController`
- `HistoricoManutencaoDispositivoController`
- `LogInteracaoIAController`
- `SessaoECGRawDataController`

### Padrão de rotas

Em geral, as rotas seguem este formato:

- `GET /api/Entidade`
- `GET /api/Entidade/{id}`
- `POST /api/Entidade`
- `PUT /api/Entidade/{id}`
- `DELETE /api/Entidade/{id}`

### Ajuste importante do Login

O `POST /api/Login` não usa a entidade inteira como contrato de entrada. Ele usa `LoginCreateRequest` para receber apenas:

- `email`
- `senhaHash`
- `tipoUsuario`
- `dataCriacao` opcional

Isso evitou o problema de binding e tornou o endpoint mais simples de consumir.

## String de conexão

O projeto usa SQL Server real configurado em `appsettings.json`.

A configuração contém:

- servidor SQL Server na rede local
- banco `NeuroVestAPI`
- usuário administrador SQL
- `TrustServerCertificate=True`

Por segurança, este documento não repete credenciais sensíveis.

## Migration e banco

Foi criada a migration inicial do projeto e aplicada no SQL Server.

Fluxo executado:

- gerar migration
- revisar o snapshot
- aplicar o banco com `dotnet ef database update`

## Testes de integração

O projeto de testes está em `NeuroVestAPI.Tests`.

### Estrutura dos testes

Arquivos principais:

- `NeuroVestAPI.Tests/ApiTestFactory.cs`
- `NeuroVestAPI.Tests/ApiEndpointSmokeTests.cs`
- `NeuroVestAPI.Tests/NeuroVestAPI.Tests.csproj`

### Como os testes funcionam

- O `WebApplicationFactory<Program>` sobe a aplicação em ambiente de teste.
- O banco de testes usa SQLite em memória.
- As configurações do SQL Server são removidas no host de teste.
- O schema é criado automaticamente ao iniciar os testes.
- Cada teste faz `POST`, `GET`, `PUT` e `DELETE` conforme o cenário.
- Os registros criados são limpos ao final do teste.

### Cobertura validada

Os testes individuais cobrem:

- Login
- PerfilMedico
- PerfilPaciente
- CondicaoPreExistente
- Dispositivo
- SessaoTelemetria
- MetricasOndasEEG
- AnaliseIAeRiscos
- RecomendacaoSistema
- AlertaSistema
- LogExportacaoDados
- ParametrizacaoAlerta
- HistoricoManutencaoDispositivo
- LogInteracaoIA
- SessaoECGRawData
- Endpoints de coleção retornando `200 OK`

## Fluxo manual para popular o banco real

Depois da validação automática, foi feita uma carga manual nos endpoints reais da API local em `http://localhost:5274`.

### Ordem usada na carga

1. Criar três logins:
   - admin
   - médico
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

### Observações da carga manual

- As entidades com chave `Guid` receberam IDs explícitos quando necessário.
- As entidades com chave `long` usaram geração automática do banco.
- Os enums foram enviados no formato esperado pela API.
- A ordem dos inserts respeitou as chaves estrangeiras.
- O banco real passou a conter registros em todas as tabelas principais.

## Resultados finais da validação manual

A API real respondeu corretamente para os endpoints principais, e a verificação por `GET` mostrou contagem positiva em todas as coleções importantes.

Resultado final observado:

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

## Como executar o projeto

### 1. Restaurar dependências

```bash
dotnet restore
```

### 2. Executar a API

```bash
dotnet run
```

A aplicação sobe em ambiente de desenvolvimento e expõe a API localmente.

### 3. Executar os testes

```bash
dotnet test
```

### 4. Atualizar o banco com migrations

```bash
dotnet ef database update -c AppDbContext
```

## Endpoints principais para consumo manual

### Login

`POST /api/Login`

Exemplo de payload:

```json
{
  "email": "admin@email.com",
  "senhaHash": "Admin@123",
  "tipoUsuario": "ADMIN"
}
```

### Exemplo de criação em outras entidades

As demais entidades seguem o padrão da própria classe de modelo, enviando os campos obrigatórios e respeitando os relacionamentos de chave estrangeira.

## Conclusão

O projeto ficou com:

- API funcional.
- Contexto EF Core configurado.
- Migration aplicada no SQL Server.
- Controllers expostas.
- Testes de integração passando.
- Banco real populado manualmente.
- Documentação do processo preservada neste repositório.

Se quiser continuar a evolução do projeto, os próximos passos naturais são:

- padronizar DTOs de entrada em mais controllers,
- adicionar validação por `DataAnnotations` ou FluentValidation,
- melhorar tratamento de erros,
- documentar exemplos de request e response por endpoint,
- criar testes de negócio mais específicos além dos smoke tests.

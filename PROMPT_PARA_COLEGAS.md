# Prompt para orientar a equipe

Use este texto para pedir a outra IA ou colega para reproduzir o mesmo trabalho feito neste projeto.

## Prompt sugerido

Quero que você trabalhe no projeto ASP.NET Core Web API chamado NeuroVestAPI e execute o fluxo completo abaixo, com foco em deixar a API funcional, gerar dados no banco e validar tudo com testes manuais e automatizados.

### Objetivo geral

Quero que você confira a estrutura do projeto, identifique os arquivos principais, implemente o que estiver faltando e deixe a API pronta para uso, com banco de dados, controllers, migrations, testes e dados inseridos.

### O que deve ser feito

1. Verificar o `Program.cs` e garantir que o `DbContext` correto esteja registrado.
2. Criar ou ajustar o contexto do Entity Framework em `Models` ou em pasta equivalente.
3. Garantir que as entidades estejam mapeadas corretamente.
4. Criar ou ajustar o `.gitignore` para não subir `bin`, `obj` e a pasta `Data`.
5. Criar as controllers com scaffold para todas as entidades da API.
6. Corrigir qualquer problema de model binding ou contrato de entrada, principalmente no login.
7. Criar a migration inicial e aplicar no banco.
8. Ajustar relacionamentos que causem erro de cascade no SQL Server.
9. Criar um projeto de testes de integração separado.
10. Garantir que os testes usem um banco isolado, como SQLite in-memory.
11. Criar testes individuais para todos os endpoints da API.
12. Subir a API localmente e executar testes manuais nos endpoints reais.
13. Popular o banco com dados reais, respeitando a ordem das dependências entre entidades.
14. Confirmar com `GET` que os registros foram gravados corretamente.
15. Documentar o processo em um arquivo `.md` no repositório.

### Regras importantes

- Não use o banco de teste para validar a persistência final.
- Não misture SQL Server real com SQLite no mesmo host de teste.
- Se um endpoint exigir enum, envie o valor no formato correto esperado pelo model binder.
- Se uma entidade tiver chave `Guid`, envie o ID explicitamente quando necessário.
- Respeite a ordem de criação das entidades para não quebrar foreign keys.
- Faça alterações pequenas e valide cada etapa antes de avançar.

### Ordem recomendada para popular o banco

1. Criar os logins necessários.
2. Criar `PerfilMedico`.
3. Criar `PerfilPaciente`.
4. Criar `Dispositivo`.
5. Criar `SessaoTelemetria`.
6. Criar `MetricasOndasEEG`.
7. Criar `AnaliseIAeRiscos`.
8. Criar `RecomendacaoSistema`.
9. Criar `AlertaSistema`.
10. Criar `LogExportacaoDados`.
11. Criar `ParametrizacaoAlerta`.
12. Criar `HistoricoManutencaoDispositivo`.
13. Criar `LogInteracaoIA`.
14. Criar `SessaoECGRawData`.
15. Criar `CondicaoPreExistente`.

### Resultado esperado

No final, eu quero:
- A API funcionando localmente.
- O banco real populado com dados.
- Os endpoints validados.
- Os testes de integração passando.
- Um arquivo `.md` com o passo a passo de tudo que foi feito.

### Observação final

Se você encontrar algum erro de build, de migration, de model binding ou de relacionamento entre tabelas, corrija a causa raiz e valide novamente antes de seguir.

## Versão curta do prompt

Se quiser uma versão menor, use isto:

Quero que você organize e finalize o projeto NeuroVestAPI. Verifique o `Program.cs`, crie ou ajuste o `DbContext`, gere as controllers, crie a migration, aplique o banco, monte testes de integração isolados com SQLite, faça testes individuais para todos os endpoints e depois faça uma carga manual no banco real, seguindo a ordem correta das dependências entre entidades. No final, confirme com `GET` que os dados foram gravados e documente tudo em um arquivo `.md`.

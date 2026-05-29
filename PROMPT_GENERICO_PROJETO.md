# Prompt genérico para reproduzir o fluxo do projeto

Use este texto para qualquer colega ou outra IA que precise executar o mesmo tipo de trabalho em uma API diferente, mudando apenas o nome do projeto e da API.

## Prompt

Quero que você trabalhe no projeto ASP.NET Core Web API chamado **[NOME_DA_API]** e faça o fluxo completo abaixo.

O banco de dados é o mesmo já definido para o grupo, e o que pode mudar entre os projetos é apenas o nome da API, a estrutura interna do projeto e os arquivos já existentes. Quero que você siga a arquitetura atual do repositório, sem apagar o que já existe sem necessidade.

### Objetivo

Quero que a aplicação fique pronta para uso, com:
- `AppDbContext` configurado corretamente.
- Controllers criadas ou ajustadas.
- Migration criada e aplicada.
- Banco atualizado.
- Testes criados e executados.
- Banco populado com dados de teste ou dados reais de validação.
- Documentação do processo em arquivo `.md`.

### O que você deve verificar e fazer

1. Verificar o arquivo de inicialização da API, normalmente `Program.cs`.
2. Conferir se o `AppDbContext` existe e está registrado no container de dependência.
3. Verificar a string de conexão e o banco configurado.
4. Criar ou ajustar o `AppDbContext` e os `DbSet` das entidades.
5. Ajustar os relacionamentos e o mapeamento das entidades no Entity Framework.
6. Criar ou gerar as controllers para todas as entidades expostas pela API.
7. Ajustar os contratos de entrada quando alguma rota estiver recebendo o modelo errado.
8. Criar a migration inicial ou a próxima migration necessária.
9. Aplicar a migration no banco real.
10. Criar um projeto de testes separado, se ainda não existir.
11. Fazer os testes de integração usarem um banco isolado, como SQLite in-memory.
12. Criar testes individuais para cada endpoint principal.
13. Subir a API localmente e fazer validação manual dos endpoints.
14. Popular o banco com dados respeitando as dependências entre as entidades.
15. Confirmar com requisições `GET` que os dados ficaram gravados.
16. Registrar tudo em um arquivo `.md` com passo a passo.

### Regras importantes

- Não misture o banco real com o banco de teste.
- Se o projeto tiver conflito de cascade no SQL Server, ajuste os relacionamentos para evitar erro de múltiplos caminhos.
- Se alguma entidade usar `Guid`, envie ou gere o ID corretamente.
- Se algum campo for `enum`, use o formato que a API espera.
- Crie os dados na ordem correta para respeitar as chaves estrangeiras.
- Faça validação depois de cada etapa importante.
- Se encontrar erro de build, migration ou binding, corrija a causa raiz antes de seguir.

### Ordem sugerida para popular o banco

1. Criar os logins base.
2. Criar perfis de médico e paciente.
3. Criar os dispositivos.
4. Criar as sessões de telemetria.
5. Criar as métricas relacionadas.
6. Criar análises de risco.
7. Criar recomendações do sistema.
8. Criar alertas.
9. Criar logs de exportação.
10. Criar parametrizações de alerta.
11. Criar históricos de manutenção.
12. Criar logs de interação com IA.
13. Criar dados brutos de ECG.
14. Criar condições pré-existentes.
15. Validar tudo com `GET`.

### Resultado esperado

No final, eu quero que você entregue:
- A API funcionando.
- O banco atualizado e populado.
- Os endpoints testados.
- Um arquivo `.md` com toda a documentação do processo.

### Versão curta do prompt

Se quiser usar uma versão menor, copie isto:

Quero que você revise a API [NOME_DA_API], confira o `AppDbContext`, ajuste ou crie as controllers, gere a migration, aplique no banco, crie testes de integração com banco isolado, valide os endpoints e popular o banco seguindo a ordem correta das dependências entre entidades. No final, documente todo o processo em um arquivo `.md`.

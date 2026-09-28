# DeskFlow API

## Sobre o projeto

A **DeskFlow API** é uma Web API RESTful para gerenciamento de chamados e suporte técnico de TI.

O sistema terá como objetivo centralizar o cadastro de categorias, a abertura e o acompanhamento de chamados, o controle do ciclo de vida dos atendimentos e o registro do histórico de interações da equipe de suporte.

## Dependências

- .NET SDK 10;
- ASP.NET Core Web API;
- Entity Framework Core 10;
- SQL Server Express, instância `SQLEXPRESS`;
- Swagger/OpenAPI para documentação e testes da API.

## Migrations

As alterações do banco devem ser controladas por migrations do Entity Framework Core.

Para aplicar as migrations existentes no SQL Server configurado, na raiz do projeto, execute:

```powershell
dotnet ef database update --project .\src\Deskflow.API 
```

Caso o comando `dotnet ef` ainda não esteja disponível, instale a ferramenta uma única vez:

```powershell
dotnet tool install --global dotnet-ef
```

## Execução

Na raiz do projeto, execute:

```powershell
dotnet run --project .\src\Deskflow.API
```

Depois, acesse a URL exibida no terminal e acrescente `/swagger` para abrir a documentação interativa.

## Endpoint implementado

### Criar categoria

```text
POST /api/category
```

Exemplo de corpo:

```json
{
  "name": "Hardware"
}
```

Uma criação bem-sucedida retorna `201 Created`, com o identificador e o nome da categoria criada.

### Listar categorias

```text
GET /api/category
```

Retorna `200 OK` com uma lista de categorias. Quando não existem registros, a resposta é uma lista vazia.

### Consultar categoria por ID

```text
GET /api/category/{id}
```

Retorna `200 OK` quando a categoria existe e `404 Not Found` quando o identificador não corresponde a uma categoria cadastrada.

### Atualizar categoria

```text
PUT /api/category/{id}
```

Exemplo de corpo:

```json
{
  "name": "Software"
}
```

Uma atualização bem-sucedida retorna `204 No Content`.

### Excluir categoria

```text
DELETE /api/category/{id}
```

Uma exclusão bem-sucedida retorna `204 No Content`. Categorias inexistentes retornam `404 Not Found`.

### Criar ticket

```text
POST /api/ticket
```

Exemplo de corpo:

```json
{
  "title": "Computador não liga",
  "description": "O equipamento não inicia.",
  "requester": "Lucas",
  "priority": 0,
  "categoryId": "guid-da-categoria"
}
```

Como `priority` é um enum, os valores numéricos correspondem a:

```text
0 = Low
1 = Medium
2 = High
```

O status e as datas do ticket são definidos automaticamente pela aplicação. Uma criação bem-sucedida retorna `201 Created`.

### Listar tickets

```text
GET /api/ticket
```

Retorna `200 OK` com os tickets cadastrados. A consulta aceita filtros opcionais combinados por status, prioridade e categoria:

```text
GET /api/ticket?status=InProgress&priority=High&categoryId={id}
```

Os filtros disponíveis são:

```text
status      = Open | InProgress | Closed
priority    = Low | Medium | High
categoryId  = identificador GUID da categoria
```

Quando nenhum filtro é informado, todos os tickets são retornados. Quando mais de um filtro é informado, eles são combinados na mesma consulta.

### Consultar ticket por ID

```text
GET /api/ticket/{id}
```

Retorna `200 OK` quando o ticket existe e `404 Not Found` quando o identificador não corresponde a um ticket cadastrado.

A consulta por ID retorna os dados completos do ticket, incluindo a categoria associada e a lista de interações.

### Iniciar atendimento

```text
PATCH /api/ticket/{id}/start
```

Não é necessário enviar corpo na requisição. A operação permite a transição:

```text
Open -> InProgress
```

Tickets inexistentes retornam `404 Not Found`. Tickets que não estejam com status `Open` não podem ser iniciados.

### Encerrar atendimento

```text
PATCH /api/ticket/{id}/close
```

Exemplo de corpo:

```json
{
  "solution": "O problema foi resolvido."
}
```

O encerramento só é permitido para tickets `InProgress`. A operação altera o status para `Closed`, registra a solução e preenche `ClosedAt`. Uma operação bem-sucedida retorna `204 No Content`.

### Criar interação em um ticket

```text
POST /api/ticket/{ticketId}/interactions
```

Exemplo de corpo:

```json
{
  "message": "Foi solicitado o teste do equipamento.",
  "author": "Lucas"
}
```

A interação deve estar vinculada a um ticket existente e que não esteja fechado. Uma criação bem-sucedida retorna `201 Created`.

### Listar interações de um ticket

```text
GET /api/ticket/{ticketId}/interactions
```

Retorna `200 OK` com as interações cadastradas para o ticket. Tickets inexistentes retornam `404 Not Found`.

## Organização planejada

O projeto será desenvolvido em camadas, separando responsabilidades entre Controllers, Services, Repositories, Models, Data e Middlewares.

## Status atual

- Entidade, Repository e Service de categorias implementados;
- DTOs de entrada e saída de categorias implementados;
- Migration inicial criada para a tabela `Categories`;
- Endpoints CRUD de categorias implementados;
- Entidade, Repository e Service básicos de tickets implementados;
- DTOs de entrada e saída de tickets implementados;
- Migrations criadas para tickets e solução do atendimento;
- Migration criada para a tabela `Interactions`;
- Endpoints de criação, listagem e consulta de tickets implementados;
- Consulta detalhada de ticket com categoria e interações implementada;
- Endpoints de início e encerramento do atendimento implementados;
- Entidade, Repository e Service de interações implementados;
- DTOs de entrada e saída de interações implementados;
- Endpoints de criação e listagem de interações implementados;
- Filtros dinâmicos de tickets por status, prioridade e categoria implementados;
- Testes manuais dos endpoints realizados pelo Swagger;
- A validação para impedir exclusão de categorias vinculadas a chamados será concluída após a criação da entidade `Chamado`;
- Tratamento global de erros será implementado na próxima etapa.

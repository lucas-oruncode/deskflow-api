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

## Organização planejada

O projeto será desenvolvido em camadas, separando responsabilidades entre Controllers, Services, Repositories, Models, Data e Middlewares.

## Status atual

- Entidade, Repository e Service de categorias implementados;
- DTOs de entrada e saída de categorias implementados;
- Migration inicial criada para a tabela `Categories`;
- Endpoint `POST /api/category` implementado;
- Os demais endpoints de categorias, chamados, interações, filtros e tratamento global de erros serão implementados nas próximas etapas.

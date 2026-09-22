# DeskFlow API

## Sobre o projeto

A **DeskFlow API** é uma Web API RESTful para gerenciamento de chamados e suporte técnico de TI.

O sistema terá como objetivo centralizar o cadastro de categorias, a abertura e o acompanhamento de chamados, o controle do ciclo de vida dos atendimentos e o registro do histórico de interações da equipe de suporte.

## Dependências

- .NET SDK 10;
- ASP.NET Core Web API;
- SQL Server Express, instância `SQLEXPRESS`.

## Configuração concluída

- A estrutura inicial em camadas foi criada.
- O `AppDbContext` foi criado em `Data/AppDbContext.cs`.
- O contexto foi registrado no `Program.cs`.
- A connection string `DefaultConnection` foi configurada no `appsettings.json`.

## Organização planejada

O projeto será desenvolvido em camadas, separando responsabilidades entre Controllers, Services, Repositories, Models, Data e Middlewares.

## Status atual

As funcionalidades de categorias, chamados, interações, filtros, migrations e tratamento global de erros serão implementadas nas próximas etapas.

# 🏗️ Order Management — CQRS & Clean Architecture

> Projeto de estudo aprofundado sobre **CQRS** e **Clean Architecture** com **.NET 9**, demonstrando como separar operações de leitura e escrita para máxima performance, escalabilidade e manutenibilidade.

---

## 📋 Sumário

- [Visão Geral](#-visão-geral)
- [Tecnologias](#-tecnologias)
- [Arquitetura](#-arquitetura)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Pré-requisitos](#-pré-requisitos)
- [Como Executar](#-como-executar)
- [Conceitos Aprendidos](#-conceitos-aprendidos)
- [Testes](#-testes)
- [Autor](#-autor)

---

## 🎯 Visão Geral

Este projeto implementa um sistema de **gestão de pedidos** robusto, construído com foco em boas práticas de arquitetura de software.

O diferencial está no uso do **CQRS**: as escritas (**Commands**) são processadas via **Entity Framework Core** no SQL Server, enquanto as leituras (**Queries**) são otimizadas via **Dapper** — utilizando o mesmo banco de dados, mas com fluxos lógicos completamente separados. Isso garante que cada lado possa evoluir, escalar e ser otimizado de forma independente.

```
[Cliente]
    │
    ▼
  [API]
    │
    ├── (Command) ──▶ [MediatR] ──▶ [Handler] ──▶ [EF Core] ──▶ [SQL Server]
    │                                                                  │
    └── (Query)  ──▶ [MediatR] ──▶ [Handler] ──▶ [Dapper]  ◀─────────┘
```

---

## 🚀 Tecnologias

| Tecnologia | Versão | Função |
|---|---|---|
| .NET SDK | 9.0 | Runtime e compilação |
| Entity Framework Core | 9.0 | ORM para operações de escrita (Commands) |
| Dapper | 2.1+ | Micro-ORM para operações de leitura (Queries) |
| MediatR | 12.0+ | Padrão Mediator para desacoplamento entre camadas |
| FluentValidation | 11.0+ | Validação declarativa de comandos |
| SQL Server Edge | 2022 | Banco de dados relacional (via Docker) |
| xUnit + Moq | latest | Testes unitários e mocking de dependências |
| Swashbuckle | 7.2.0 | Documentação interativa via Swagger |

> **Plataforma:** Otimizado para macOS Apple Silicon (M-series) usando Azure SQL Edge.

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, organizando o código em camadas com responsabilidades bem definidas e dependências apontando sempre para dentro.

```
┌────────────────────────────────────────────┐
│                    API                     │  ← Porta de entrada (Controllers)
├────────────────────────────────────────────┤
│               Application                 │  ← Casos de uso (Commands, Queries, Handlers)
├────────────────────────────────────────────┤
│                  Domain                   │  ← Núcleo do negócio (Entidades, Eventos, Interfaces)
├────────────────────────────────────────────┤
│              Infrastructure               │  ← Implementações técnicas (EF Core, Dapper, DB)
└────────────────────────────────────────────┘
```

### Responsabilidades por camada

**`Domain`** — O coração do sistema. Contém Entidades ricas (ex: `Order` com o método `Cancel()`), Eventos de Domínio e interfaces de Repositório. É 100% C# puro, sem dependências externas.

**`Application`** — Lógica de orquestração. Aqui vivem os Commands, Queries, Handlers, DTOs (`OrderResponse`) e validadores. Depende apenas do Domain.

**`Infrastructure`** — Implementação técnica: `DbContext` do EF Core, consultas SQL via Dapper, Migrations e registro de dependências.

**`API`** — Controllers enxutos que apenas recebem requisições HTTP e as despacham para o MediatR. Sem lógica de negócio.

---

## 📁 Estrutura do Projeto

```
OrderManagementCQRS/
├── src/
│   ├── API/                    # Controllers, Middlewares e configuração da aplicação
│   ├── Application/            # Commands, Queries, Handlers e Validators (FluentValidation)
│   ├── Domain/                 # Entidades, Eventos de Domínio e Interfaces de Repositório
│   └── Infrastructure/         # DbContext, Dapper, Migrations e implementação dos Repositórios
├── tests/
│   └── Application.Tests/      # Testes unitários com xUnit e Moq
└── docker-compose.yml
```

---

## 🛠️ Pré-requisitos

Certifique-se de ter instalado:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- EF Core CLI:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## ▶️ Como Executar

### 1. Subir o SQL Server via Docker

O projeto usa **Azure SQL Edge**, otimizado para chips Apple Silicon (M1/M2/M3).

```bash
docker-compose up -d
```

### 2. Aplicar as Migrations ao banco de dados

Isso criará a estrutura de tabelas no container Docker.

```bash
dotnet ef database update --project src/Infrastructure --startup-project src/API
```

### 3. Executar a API

```bash
dotnet run --project src/API
```

Acesse a documentação interativa em: **http://localhost:5079/swagger**

> A porta pode variar — verifique o terminal após o `dotnet run`.

---

## 📚 Conceitos Aprendidos

### ⚡ CQRS (Command Query Responsibility Segregation)
Separação total entre a lógica que **altera** dados (Commands via EF Core) e a lógica que **busca** dados (Queries via Dapper). Cada lado pode ser otimizado, escalado e mantido de forma independente.

### 🛡️ Domain Driven Design (DDD)
Uso de **Rich Domain Models**: a lógica de negócio (como o método `Cancel()`) fica dentro da entidade `Order`, e não espalhada em serviços anêmicos.

### 📢 Domain Events
Implementação de eventos de domínio para disparar ações colaterais de forma desacoplada — por exemplo, registrar um log ou simular envio de e-mail ao criar um pedido.

### 🧼 Clean Architecture
Organização das camadas de forma que as dependências fluam sempre em direção ao Domain, garantindo que o núcleo do negócio não conheça detalhes de infraestrutura ou apresentação.

### 🏗️ Global Exception Handling
Uso de **Middlewares customizados** para capturar exceções em qualquer camada e retornar uma resposta JSON padronizada e consistente para o cliente.

### 🔗 Padrão Mediator com MediatR
Desacoplamento total entre Controllers e Handlers: o Controller apenas publica uma mensagem, e o MediatR se encarrega de roteá-la para o Handler correto.

### ✅ Validação com FluentValidation
Validação declarativa e expressiva dos Commands, com regras de negócio claras e mensagens de erro padronizadas.

---

## 🧪 Testes

Os testes unitários ficam em `tests/Application.Tests` e cobrem os Handlers da camada Application.

```bash
dotnet test
```

Ferramentas utilizadas:
- **xUnit** — Framework de testes
- **Moq** — Mocking de dependências (repositórios, serviços externos)

---

## 👨‍💻 Autor

Desenvolvido por **Juliano Galhardo** como parte de estudos aprofundados sobre arquitetura de software e alta performance com .NET.

[![LinkedIn](https://img.shields.io/badge/LinkedIn-Juliano%20Galhardo-0077B5?style=flat&logo=linkedin)](https://linkedin.com/in/julianogalhardo9)
[![GitHub](https://img.shields.io/badge/GitHub-@julianogalhardo-181717?style=flat&logo=github)](https://github.com/julianogalhardo)

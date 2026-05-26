# ModaCircular

O **ModaCircular** é uma aplicação web para cadastro e gerenciamento de roupas disponíveis para **troca** ou **doação**.

A ideia do projeto é simples: permitir que roupas em bom estado sejam cadastradas, consultadas, editadas e removidas de forma prática. A aplicação organiza as peças por categoria, tamanho, estado de conservação, tipo de disponibilidade e localização.

Este projeto foi desenvolvido como trabalho prático da disciplina **Arquitetura de Aplicações Web — 2026.1**, com foco na construção de uma aplicação web completa utilizando API REST, banco de dados NoSQL, documentação Swagger/OpenAPI e consumo de dados pelo frontend sem recarregar a página.

---

## Objetivo

O objetivo do ModaCircular é demonstrar os principais conceitos estudados na disciplina, incluindo:

- criação de uma API REST;
- implementação de CRUD completo;
- persistência em banco de dados NoSQL;
- documentação da API com Swagger/OpenAPI;
- consumo da API pelo frontend usando JavaScript e `fetch`;
- atualização dinâmica da tela sem recarregar a página;
- organização do backend em camadas.

---

## Domínio da aplicação

O sistema possui duas entidades principais:

### Categoria

Representa o tipo da roupa cadastrada.

Exemplos:

- Camisetas
- Calças
- Vestidos

### Roupa

Representa uma peça disponível para troca ou doação.

Cada roupa possui informações como:

- título;
- descrição;
- imagem;
- tipo: troca ou doação;
- tamanho;
- estado de conservação;
- gênero;
- estado;
- cidade;
- bairro;
- categoria;
- disponibilidade.

A relação entre as entidades é:

> Uma categoria pode possuir várias roupas, mas cada roupa pertence a uma única categoria.

---

## Tecnologias utilizadas

### Backend

- .NET 10
- C#
- ASP.NET Core Web API
- Swagger/OpenAPI

### Banco de dados

- MongoDB
- Docker Compose

### Frontend

- HTML
- CSS
- JavaScript
- Fetch API

### Versionamento

- Git
- GitHub

---

## Pré-requisitos

Para executar o projeto localmente, é necessário ter instalado:

- .NET 10 SDK;
- Docker Desktop;
- Git;
- Visual Studio Code;
- Extensão Live Server no VS Code.

---

## Como executar o projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/carolpaassos/moda-circular.git
cd moda-circular
```

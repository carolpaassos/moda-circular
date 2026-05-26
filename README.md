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

Esses dados podem ser cadastrados pelo próprio frontend ou diretamente pelo Swagger, utilizando os endpoints de categorias e roupas.

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
---

### 2. Subir o MongoDB

Na raiz do projeto, execute:

```bash
docker compose up -d
```

Para conferir se o container está rodando:

```bash
docker ps
```

O container esperado é `moda-circular-mongodb`.

---

### 3. Configurar a variável de ambiente

A aplicação utiliza uma variável de ambiente para configurar a conexão com o MongoDB.

No PowerShell, execute:

```powershell
$env:MONGODB_CONNECTION_STRING="mongodb://admin:admin123@localhost:27017/?authSource=admin"
```

Essa variável é usada pela API para acessar o banco de dados sem deixar a string de conexão fixa diretamente no código.

Exemplo de configuração:

```txt
MONGODB_CONNECTION_STRING=mongodb://usuario:senha@localhost:27017/?authSource=admin
```

---

### 4. Rodar a API

Acesse a pasta da API:

```bash
cd backend/ModaCircular.Api
```

Execute:

```bash
dotnet run
```

Com a aplicação em execução, a API ficará disponível em:

```txt
http://localhost:5222
```

---

## Documentação Swagger

Com a API rodando, acesse no navegador:

```txt
http://localhost:5222/swagger
```

Pelo Swagger é possível testar os endpoints de categorias, roupas e também o endpoint de verificação da conexão com o MongoDB.

---

## Como abrir o frontend

Com a API rodando, abra o projeto no Visual Studio Code:

```bash
code .
```

Depois abra o arquivo:

```txt
frontend/index.html
```

Clique com o botão direito no arquivo e selecione:

```txt
Open with Live Server
```

O frontend será aberto em um endereço parecido com:

```txt
http://127.0.0.1:5500/frontend/index.html
```

---

## Funcionalidades do frontend

O frontend permite:

- listar roupas disponíveis;
- visualizar detalhes de uma roupa;
- cadastrar uma nova roupa;
- editar uma roupa existente;
- excluir uma roupa;
- filtrar roupas por cidade, tipo e tamanho;
- listar categorias cadastradas.

As ações são realizadas com JavaScript usando `fetch`, permitindo que a tela seja atualizada sem recarregar a página inteira.

---

## Endpoints principais

### Categorias

```http
GET    /categorias
GET    /categorias/{id}
POST   /categorias
PUT    /categorias/{id}
DELETE /categorias/{id}
```

### Roupas

```http
GET    /roupas
GET    /roupas/{id}
POST   /roupas
PUT    /roupas/{id}
DELETE /roupas/{id}
```

### Verificação da conexão com MongoDB

```http
GET /health/mongodb
```

---

## Dados utilizados na demonstração

Durante o desenvolvimento e testes, foram utilizadas as seguintes categorias:

- Camisetas;
- Calças;
- Vestidos.

E as seguintes roupas:

- Camiseta Branca;
- Calça Jeans;
- Vestido.

Cada roupa possui informações como tipo, tamanho, estado de conservação, localização, disponibilidade e imagem.

---

## Imagens das roupas

As imagens utilizadas no frontend ficam armazenadas dentro da pasta:

```txt
frontend/img
```

No banco de dados, a roupa armazena apenas o caminho da imagem por meio do campo `imagemUrl`.

Exemplo:

```json
{
  "imagemUrl": "http://127.0.0.1:5500/frontend/img/camiseta_branca.webp"
}
```

Essa abordagem foi utilizada para manter o projeto simples, sem implementar upload de arquivos, já que o foco principal do trabalho é API REST, banco NoSQL, Swagger e consumo de dados pelo frontend.

---

## Organização do backend

O backend foi organizado em camadas para separar melhor as responsabilidades:

- `Controllers`: recebem as requisições HTTP e retornam as respostas da API;
- `Services`: concentram as regras de negócio;
- `Repositories`: fazem a comunicação com o MongoDB;
- `Models`: representam os documentos salvos no banco;
- `DTOs`: definem os dados recebidos nas requisições;
- `Settings`: armazena as configurações relacionadas ao MongoDB.

Essa organização deixa o código mais limpo, facilita a manutenção e ajuda na evolução futura da aplicação.

---

## Banco de dados

O projeto utiliza o MongoDB como banco de dados NoSQL.

O banco utilizado pela aplicação é:

```txt
ModaCircularDb
```

As principais coleções são:

```txt
Categorias
Roupas
```

O MongoDB é executado localmente por meio do Docker Compose.

---

## Bônus implementados

### Bônus C — Testes Unitários

O projeto possui testes unitários para validar regras de negócio da camada de serviço da API.

Foram implementados testes para os seguintes cenários:

- criação de categoria com dados válidos;
- tentativa de criação de categoria com nome duplicado;
- criação de roupa com categoria existente;
- tentativa de criação de roupa com categoria inexistente.

Os testes foram criados com xUnit e podem ser executados, a partir da raiz do projeto, com o comando:

```bash
dotnet test ModaCircular.slnx

### Bônus D — Princípios SOLID

O projeto aplica princípios SOLID na organização do backend, principalmente por meio da separação em camadas e uso de interfaces.

Foram documentados os seguintes princípios:

- **S — Single Responsibility Principle**
- **D — Dependency Inversion Principle**
- **I — Interface Segregation Principle**

A explicação completa está no arquivo [`SOLID.md`](./SOLID.md).

---

## Autor

Projeto desenvolvido para a disciplina **Arquitetura de Aplicações Web — 2026.1**.

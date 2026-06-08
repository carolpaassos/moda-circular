# Princípios SOLID aplicados no ModaCircular

Este documento apresenta os princípios SOLID aplicados no backend da aplicação ModaCircular.

A aplicação foi organizada em camadas para separar responsabilidades, facilitar manutenção e deixar o código mais claro para futuras evoluções.

---

## 1. S — Single Responsibility Principle

O princípio da responsabilidade única diz que uma classe deve ter apenas uma responsabilidade principal.

No ModaCircular, esse princípio foi aplicado separando o backend em camadas específicas.

### Onde aparece

- `Controllers/CategoriasController.cs`
- `Controllers/RoupasController.cs`
- `Services/CategoriaService.cs`
- `Services/RoupaService.cs`
- `Repositories/CategoriaRepository.cs`
- `Repositories/RoupaRepository.cs`

### Justificativa

Os controllers são responsáveis apenas por receber as requisições HTTP e retornar respostas adequadas para o cliente.

As services concentram as regras de negócio da aplicação, como validar se uma categoria existe antes de cadastrar uma roupa.

Os repositories são responsáveis pela comunicação com o MongoDB.

Com essa separação, cada classe tem uma responsabilidade bem definida, evitando concentrar regras de negócio, acesso ao banco e controle de requisições em um único lugar.

---

## 2. D — Dependency Inversion Principle

O princípio da inversão de dependência diz que classes de alto nível não devem depender diretamente de classes concretas, mas sim de abstrações.

### Onde aparece

- `Services/CategoriaService.cs`
- `Services/RoupaService.cs`
- `Repositories/ICategoriaRepository.cs`
- `Repositories/IRoupaRepository.cs`
- `Program.cs`

### Justificativa

As services não dependem diretamente das classes concretas dos repositories. Elas dependem de interfaces.

Exemplo em `CategoriaService.cs`:

```csharp
private readonly ICategoriaRepository _categoriaRepository;
## 3. I — Interface Segregation Principle

O princípio da segregação de interfaces diz que é melhor ter interfaces menores e específicas do que uma interface grande e genérica com métodos que nem todas as classes precisam utilizar.

No ModaCircular, esse princípio foi aplicado separando as interfaces de acordo com a responsabilidade de cada entidade e camada do sistema.

### Onde aparece

- `Repositories/ICategoriaRepository.cs`
- `Repositories/IRoupaRepository.cs`
- `Services/ICategoriaService.cs`
- `Services/IRoupaService.cs`

### Justificativa

Em vez de criar uma única interface genérica para todas as operações do sistema, o projeto possui interfaces específicas para cada contexto.

A interface `ICategoriaRepository` define apenas os métodos relacionados ao acesso de dados de categorias.

A interface `IRoupaRepository` define apenas os métodos relacionados ao acesso de dados de roupas.

O mesmo acontece na camada de services, onde `ICategoriaService` concentra os contratos relacionados às regras de categoria, enquanto `IRoupaService` concentra os contratos relacionados às regras de roupa.

Com isso, cada classe depende apenas dos métodos que realmente precisa utilizar, evitando acoplamento desnecessário e deixando o código mais organizado e fácil de manter.

### Exemplo no projeto

A `CategoriaService` depende apenas de `ICategoriaRepository`, pois trabalha com regras relacionadas a categorias.

A `RoupaService` depende de `IRoupaRepository` e também de `ICategoriaRepository`, porque ao cadastrar uma roupa é necessário validar se a categoria informada existe.

Essa separação evita uma interface única e grande para todo o sistema, mantendo cada contrato mais simples, específico e coerente com sua responsabilidade.
:::
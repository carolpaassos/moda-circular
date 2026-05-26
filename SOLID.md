# Princípios SOLID aplicados no ModaCircular

Este documento apresenta alguns princípios SOLID aplicados no backend da aplicação ModaCircular.

O objetivo da organização do código foi separar responsabilidades, facilitar manutenção e deixar a API mais clara para evolução futura.

---

## 1. S — Single Responsibility Principle

O princípio da responsabilidade única diz que cada classe deve ter um único motivo para mudar.

No projeto, esse princípio foi aplicado separando as responsabilidades em camadas.

### Onde aparece

- `Controllers/CategoriasController.cs`
- `Controllers/RoupasController.cs`
- `Services/CategoriaService.cs`
- `Services/RoupaService.cs`
- `Repositories/CategoriaRepository.cs`
- `Repositories/RoupaRepository.cs`

### Justificativa

Os controllers são responsáveis apenas por receber as requisições HTTP e retornar respostas adequadas.

As services concentram as regras de negócio, como validar se uma categoria existe antes de cadastrar uma roupa.

Os repositories são responsáveis pela comunicação com o MongoDB.

Com essa separação, uma alteração na regra de negócio não precisa ser feita diretamente no controller ou no código de acesso ao banco.

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

As services não dependem diretamente das classes concretas dos repositories. Elas dependem das interfaces `ICategoriaRepository` e `IRoupaRepository`.

Exemplo:

```csharp
private readonly ICategoriaRepository _categoriaRepository;
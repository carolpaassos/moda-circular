using ModaCircular.Api.DTOs;
using ModaCircular.Api.Models;
using ModaCircular.Api.Repositories;
using ModaCircular.Api.Services;

namespace ModaCircular.Tests;

public class ServicesTests
{
    [Fact]
    public async Task CriarCategoria_DeveCriarCategoria_QuandoDadosForemValidos()
    {
        var repository = new FakeCategoriaRepository();
        var service = new CategoriaService(repository);

        var dto = new CategoriaCreateDto
        {
            Nome = "Camisetas",
            Descricao = "Peças superiores casuais",
            Ativa = true
        };

        var categoria = await service.CriarAsync(dto);

        Assert.NotNull(categoria);
        Assert.Equal("Camisetas", categoria.Nome);
        Assert.Single(repository.Categorias);
    }

    [Fact]
    public async Task CriarCategoria_DeveGerarErro_QuandoNomeJaExistir()
    {
        var repository = new FakeCategoriaRepository();

        repository.Categorias.Add(new Categoria
        {
            Id = "cat1",
            Nome = "Camisetas",
            Descricao = "Categoria já cadastrada",
            Ativa = true
        });

        var service = new CategoriaService(repository);

        var dto = new CategoriaCreateDto
        {
            Nome = "Camisetas",
            Descricao = "Nova tentativa de cadastro",
            Ativa = true
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.CriarAsync(dto));
    }

    [Fact]
    public async Task CriarRoupa_DeveCriarRoupa_QuandoCategoriaExistir()
    {
        var categoriaRepository = new FakeCategoriaRepository();

        categoriaRepository.Categorias.Add(new Categoria
        {
            Id = "cat1",
            Nome = "Camisetas",
            Descricao = "Peças superiores",
            Ativa = true
        });

        var roupaRepository = new FakeRoupaRepository();
        var service = new RoupaService(roupaRepository, categoriaRepository);

        var dto = new RoupaCreateDto
        {
            Titulo = "Camiseta Branca",
            Descricao = "Camiseta branca em ótimo estado",
            ImagemUrl = "img/camiseta_branca.webp",
            Tipo = "Doação",
            Tamanho = "M",
            EstadoConservacao = "Ótimo",
            Genero = "Unissex",
            Estado = "MG",
            Cidade = "Belo Horizonte",
            Bairro = "Centro",
            CategoriaId = "cat1",
            Disponivel = true
        };

        var roupa = await service.CriarAsync(dto);

        Assert.NotNull(roupa);
        Assert.Equal("Camiseta Branca", roupa.Titulo);
        Assert.Equal("cat1", roupa.CategoriaId);
        Assert.Single(roupaRepository.Roupas);
    }

    [Fact]
    public async Task CriarRoupa_DeveGerarErro_QuandoCategoriaNaoExistir()
    {
        var categoriaRepository = new FakeCategoriaRepository();
        var roupaRepository = new FakeRoupaRepository();
        var service = new RoupaService(roupaRepository, categoriaRepository);

        var dto = new RoupaCreateDto
        {
            Titulo = "Vestido",
            Descricao = "Vestido em bom estado",
            ImagemUrl = "img/vestido.webp",
            Tipo = "Doação",
            Tamanho = "P",
            EstadoConservacao = "Bom",
            Genero = "Feminino",
            Estado = "MG",
            Cidade = "Belo Horizonte",
            Bairro = "Caiçara",
            CategoriaId = "categoria-inexistente",
            Disponivel = true
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.CriarAsync(dto));
    }
}

public class FakeCategoriaRepository : ICategoriaRepository
{
    public List<Categoria> Categorias { get; } = new();

    public Task<List<Categoria>> ListarAsync()
    {
        return Task.FromResult(Categorias);
    }

    public Task<Categoria?> BuscarPorIdAsync(string id)
    {
        var categoria = Categorias.FirstOrDefault(categoria => categoria.Id == id);
        return Task.FromResult(categoria);
    }

    public Task<Categoria?> BuscarPorNomeAsync(string nome)
    {
        var categoria = Categorias.FirstOrDefault(categoria =>
            categoria.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(categoria);
    }

    public Task CriarAsync(Categoria categoria)
    {
        categoria.Id ??= Guid.NewGuid().ToString();
        Categorias.Add(categoria);
        return Task.CompletedTask;
    }

    public Task AtualizarAsync(string id, Categoria categoria)
    {
        var index = Categorias.FindIndex(item => item.Id == id);

        if (index >= 0)
        {
            Categorias[index] = categoria;
        }

        return Task.CompletedTask;
    }

    public Task RemoverAsync(string id)
    {
        Categorias.RemoveAll(categoria => categoria.Id == id);
        return Task.CompletedTask;
    }
}

public class FakeRoupaRepository : IRoupaRepository
{
    public List<Roupa> Roupas { get; } = new();

    public Task<List<Roupa>> ListarAsync(string? categoriaId, string? tipo, string? tamanho, string? cidade)
    {
        var query = Roupas.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(categoriaId))
        {
            query = query.Where(roupa => roupa.CategoriaId == categoriaId);
        }

        if (!string.IsNullOrWhiteSpace(tipo))
        {
            query = query.Where(roupa => roupa.Tipo == tipo);
        }

        if (!string.IsNullOrWhiteSpace(tamanho))
        {
            query = query.Where(roupa => roupa.Tamanho == tamanho);
        }

        if (!string.IsNullOrWhiteSpace(cidade))
        {
            query = query.Where(roupa => roupa.Cidade == cidade);
        }

        return Task.FromResult(query.ToList());
    }

    public Task<Roupa?> BuscarPorIdAsync(string id)
    {
        var roupa = Roupas.FirstOrDefault(roupa => roupa.Id == id);
        return Task.FromResult(roupa);
    }

    public Task CriarAsync(Roupa roupa)
    {
        roupa.Id ??= Guid.NewGuid().ToString();
        Roupas.Add(roupa);
        return Task.CompletedTask;
    }

    public Task AtualizarAsync(string id, Roupa roupa)
    {
        var index = Roupas.FindIndex(item => item.Id == id);

        if (index >= 0)
        {
            Roupas[index] = roupa;
        }

        return Task.CompletedTask;
    }

    public Task RemoverAsync(string id)
    {
        Roupas.RemoveAll(roupa => roupa.Id == id);
        return Task.CompletedTask;
    }
}
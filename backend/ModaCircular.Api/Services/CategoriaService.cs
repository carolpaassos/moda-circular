using ModaCircular.Api.DTOs;
using ModaCircular.Api.Models;
using ModaCircular.Api.Repositories;

namespace ModaCircular.Api.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<List<Categoria>> ListarAsync()
    {
        return await _categoriaRepository.ListarAsync();
    }

    public async Task<Categoria?> BuscarPorIdAsync(string id)
    {
        return await _categoriaRepository.BuscarPorIdAsync(id);
    }

    public async Task<Categoria> CriarAsync(CategoriaCreateDto categoriaDto)
    {
        var nome = categoriaDto.Nome.Trim();

        var categoriaExistente = await _categoriaRepository.BuscarPorNomeAsync(nome);

        if (categoriaExistente != null)
        {
            throw new InvalidOperationException("Já existe uma categoria cadastrada com esse nome.");
        }

        var categoria = new Categoria
        {
            Nome = nome,
            Descricao = categoriaDto.Descricao.Trim(),
            Ativa = categoriaDto.Ativa,
            DataCriacao = DateTime.UtcNow
        };

        await _categoriaRepository.CriarAsync(categoria);

        return categoria;
    }

    public async Task<bool> AtualizarAsync(string id, CategoriaUpdateDto categoriaDto)
    {
        var categoriaExistente = await _categoriaRepository.BuscarPorIdAsync(id);

        if (categoriaExistente == null)
        {
            return false;
        }

        categoriaExistente.Nome = categoriaDto.Nome.Trim();
        categoriaExistente.Descricao = categoriaDto.Descricao.Trim();
        categoriaExistente.Ativa = categoriaDto.Ativa;

        await _categoriaRepository.AtualizarAsync(id, categoriaExistente);

        return true;
    }

    public async Task<bool> RemoverAsync(string id)
    {
        var categoriaExistente = await _categoriaRepository.BuscarPorIdAsync(id);

        if (categoriaExistente == null)
        {
            return false;
        }

        await _categoriaRepository.RemoverAsync(id);

        return true;
    }
}
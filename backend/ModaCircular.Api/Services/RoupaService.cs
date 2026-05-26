using ModaCircular.Api.DTOs;
using ModaCircular.Api.Models;
using ModaCircular.Api.Repositories;

namespace ModaCircular.Api.Services;

public class RoupaService : IRoupaService
{
    private readonly IRoupaRepository _roupaRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public RoupaService(IRoupaRepository roupaRepository, ICategoriaRepository categoriaRepository)
    {
        _roupaRepository = roupaRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<List<Roupa>> ListarAsync(string? categoriaId, string? tipo, string? tamanho, string? cidade)
    {
        return await _roupaRepository.ListarAsync(categoriaId, tipo, tamanho, cidade);
    }

    public async Task<Roupa?> BuscarPorIdAsync(string id)
    {
        return await _roupaRepository.BuscarPorIdAsync(id);
    }

    public async Task<Roupa> CriarAsync(RoupaCreateDto roupaDto)
    {
        await ValidarCategoriaAsync(roupaDto.CategoriaId);

        var roupa = new Roupa
        {
            Titulo = roupaDto.Titulo.Trim(),
            Descricao = roupaDto.Descricao.Trim(),
            Tipo = roupaDto.Tipo.Trim(),
            Tamanho = roupaDto.Tamanho.Trim(),
            EstadoConservacao = roupaDto.EstadoConservacao.Trim(),
            Genero = roupaDto.Genero.Trim(),
            Estado = roupaDto.Estado.Trim(),
            Cidade = roupaDto.Cidade.Trim(),
            Bairro = roupaDto.Bairro.Trim(),
            CategoriaId = roupaDto.CategoriaId.Trim(),
            Disponivel = roupaDto.Disponivel,
            DataCriacao = DateTime.UtcNow
        };

        await _roupaRepository.CriarAsync(roupa);

        return roupa;
    }

    public async Task<bool> AtualizarAsync(string id, RoupaUpdateDto roupaDto)
    {
        var roupaExistente = await _roupaRepository.BuscarPorIdAsync(id);

        if (roupaExistente == null)
        {
            return false;
        }

        await ValidarCategoriaAsync(roupaDto.CategoriaId);

        roupaExistente.Titulo = roupaDto.Titulo.Trim();
        roupaExistente.Descricao = roupaDto.Descricao.Trim();
        roupaExistente.Tipo = roupaDto.Tipo.Trim();
        roupaExistente.Tamanho = roupaDto.Tamanho.Trim();
        roupaExistente.EstadoConservacao = roupaDto.EstadoConservacao.Trim();
        roupaExistente.Genero = roupaDto.Genero.Trim();
        roupaExistente.Estado = roupaDto.Estado.Trim();
        roupaExistente.Cidade = roupaDto.Cidade.Trim();
        roupaExistente.Bairro = roupaDto.Bairro.Trim();
        roupaExistente.CategoriaId = roupaDto.CategoriaId.Trim();
        roupaExistente.Disponivel = roupaDto.Disponivel;

        await _roupaRepository.AtualizarAsync(id, roupaExistente);

        return true;
    }

    public async Task<bool> RemoverAsync(string id)
    {
        var roupaExistente = await _roupaRepository.BuscarPorIdAsync(id);

        if (roupaExistente == null)
        {
            return false;
        }

        await _roupaRepository.RemoverAsync(id);

        return true;
    }

    private async Task ValidarCategoriaAsync(string categoriaId)
    {
        var categoria = await _categoriaRepository.BuscarPorIdAsync(categoriaId.Trim());

        if (categoria == null)
        {
            throw new InvalidOperationException("A categoria informada não existe.");
        }

        if (!categoria.Ativa)
        {
            throw new InvalidOperationException("A categoria informada está inativa.");
        }
    }
}
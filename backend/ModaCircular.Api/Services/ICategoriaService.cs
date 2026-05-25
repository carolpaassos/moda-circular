using ModaCircular.Api.DTOs;
using ModaCircular.Api.Models;

namespace ModaCircular.Api.Services;

public interface ICategoriaService
{
    Task<List<Categoria>> ListarAsync();
    Task<Categoria?> BuscarPorIdAsync(string id);
    Task<Categoria> CriarAsync(CategoriaCreateDto categoriaDto);
    Task<bool> AtualizarAsync(string id, CategoriaUpdateDto categoriaDto);
    Task<bool> RemoverAsync(string id);
}
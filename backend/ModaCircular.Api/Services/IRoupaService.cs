using ModaCircular.Api.DTOs;
using ModaCircular.Api.Models;

namespace ModaCircular.Api.Services;

public interface IRoupaService
{
    Task<List<Roupa>> ListarAsync(string? categoriaId, string? tipo, string? tamanho, string? cidade);
    Task<Roupa?> BuscarPorIdAsync(string id);
    Task<Roupa> CriarAsync(RoupaCreateDto roupaDto);
    Task<bool> AtualizarAsync(string id, RoupaUpdateDto roupaDto);
    Task<bool> RemoverAsync(string id);
}
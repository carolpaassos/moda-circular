using ModaCircular.Api.Models;

namespace ModaCircular.Api.Repositories;

public interface IRoupaRepository
{
    Task<List<Roupa>> ListarAsync(string? categoriaId, string? tipo, string? tamanho, string? cidade);
    Task<Roupa?> BuscarPorIdAsync(string id);
    Task CriarAsync(Roupa roupa);
    Task AtualizarAsync(string id, Roupa roupa);
    Task RemoverAsync(string id);
}
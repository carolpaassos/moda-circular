using ModaCircular.Api.Models;

namespace ModaCircular.Api.Repositories;

public interface ICategoriaRepository
{
    Task<List<Categoria>> ListarAsync();
    Task<Categoria?> BuscarPorIdAsync(string id);
    Task<Categoria?> BuscarPorNomeAsync(string nome);
    Task CriarAsync(Categoria categoria);
    Task AtualizarAsync(string id, Categoria categoria);
    Task RemoverAsync(string id);
}
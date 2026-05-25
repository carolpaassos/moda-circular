using Microsoft.Extensions.Options;
using ModaCircular.Api.Models;
using ModaCircular.Api.Settings;
using MongoDB.Driver;

namespace ModaCircular.Api.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly IMongoCollection<Categoria> _categorias;

    public CategoriaRepository(IMongoDatabase database, IOptions<MongoDbSettings> settings)
    {
        var collectionName = settings.Value.CategoriasCollectionName;

        if (string.IsNullOrWhiteSpace(collectionName))
        {
            throw new InvalidOperationException("O nome da coleção de categorias não foi configurado.");
        }

        _categorias = database.GetCollection<Categoria>(collectionName);
    }

    public async Task<List<Categoria>> ListarAsync()
    {
        return await _categorias.Find(_ => true).ToListAsync();
    }

    public async Task<Categoria?> BuscarPorIdAsync(string id)
    {
        return await _categorias.Find(categoria => categoria.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Categoria?> BuscarPorNomeAsync(string nome)
    {
        return await _categorias.Find(categoria => categoria.Nome == nome).FirstOrDefaultAsync();
    }

    public async Task CriarAsync(Categoria categoria)
    {
        await _categorias.InsertOneAsync(categoria);
    }

    public async Task AtualizarAsync(string id, Categoria categoria)
    {
        await _categorias.ReplaceOneAsync(c => c.Id == id, categoria);
    }

    public async Task RemoverAsync(string id)
    {
        await _categorias.DeleteOneAsync(categoria => categoria.Id == id);
    }
}
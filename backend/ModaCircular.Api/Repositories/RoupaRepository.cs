using Microsoft.Extensions.Options;
using ModaCircular.Api.Models;
using ModaCircular.Api.Settings;
using MongoDB.Driver;

namespace ModaCircular.Api.Repositories;

public class RoupaRepository : IRoupaRepository
{
    private readonly IMongoCollection<Roupa> _roupas;

    public RoupaRepository(IMongoDatabase database, IOptions<MongoDbSettings> settings)
    {
        var collectionName = settings.Value.RoupasCollectionName;

        if (string.IsNullOrWhiteSpace(collectionName))
        {
            throw new InvalidOperationException("O nome da coleção de roupas não foi configurado.");
        }

        _roupas = database.GetCollection<Roupa>(collectionName);
    }

    public async Task<List<Roupa>> ListarAsync(string? categoriaId, string? tipo, string? tamanho, string? cidade)
    {
        var filtros = new List<FilterDefinition<Roupa>>();
        var builder = Builders<Roupa>.Filter;

        if (!string.IsNullOrWhiteSpace(categoriaId))
        {
            filtros.Add(builder.Eq(roupa => roupa.CategoriaId, categoriaId));
        }

        if (!string.IsNullOrWhiteSpace(tipo))
        {
            filtros.Add(builder.Eq(roupa => roupa.Tipo, tipo));
        }

        if (!string.IsNullOrWhiteSpace(tamanho))
        {
            filtros.Add(builder.Eq(roupa => roupa.Tamanho, tamanho));
        }

        if (!string.IsNullOrWhiteSpace(cidade))
        {
            filtros.Add(builder.Eq(roupa => roupa.Cidade, cidade));
        }

        var filtroFinal = filtros.Count > 0
            ? builder.And(filtros)
            : builder.Empty;

        return await _roupas.Find(filtroFinal).ToListAsync();
    }

    public async Task<Roupa?> BuscarPorIdAsync(string id)
    {
        return await _roupas.Find(roupa => roupa.Id == id).FirstOrDefaultAsync();
    }

    public async Task CriarAsync(Roupa roupa)
    {
        await _roupas.InsertOneAsync(roupa);
    }

    public async Task AtualizarAsync(string id, Roupa roupa)
    {
        await _roupas.ReplaceOneAsync(roupa => roupa.Id == id, roupa);
    }

    public async Task RemoverAsync(string id)
    {
        await _roupas.DeleteOneAsync(roupa => roupa.Id == id);
    }
}
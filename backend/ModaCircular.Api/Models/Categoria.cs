using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ModaCircular.Api.Models;

public class Categoria
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public bool Ativa { get; set; } = true;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
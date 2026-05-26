using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ModaCircular.Api.Models;

public class Roupa
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string ImagemUrl { get; set; } = string.Empty;

    public string Tipo { get; set; } = string.Empty;

    public string Tamanho { get; set; } = string.Empty;

    public string EstadoConservacao { get; set; } = string.Empty;

    public string Genero { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;

    public string Cidade { get; set; } = string.Empty;

    public string Bairro { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoriaId { get; set; } = string.Empty;

    public bool Disponivel { get; set; } = true;

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
namespace ModaCircular.Api.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string CategoriasCollectionName { get; set; } = string.Empty;
    public string RoupasCollectionName { get; set; } = string.Empty;
}
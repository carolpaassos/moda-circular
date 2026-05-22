using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ModaCircular.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly IMongoDatabase _database;

    public HealthController(IMongoDatabase database)
    {
        _database = database;
    }

    [HttpGet("mongodb")]
    public async Task<IActionResult> VerificarMongoDb()
    {
        await _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");

        return Ok(new
        {
            status = "Conectado",
            banco = _database.DatabaseNamespace.DatabaseName,
            mensagem = "API conectada ao MongoDB com sucesso."
        });
    }
}
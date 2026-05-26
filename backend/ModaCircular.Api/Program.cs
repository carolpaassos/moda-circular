using ModaCircular.Api.Repositories;
using ModaCircular.Api.Services;
using ModaCircular.Api.Settings;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING")
    ?? builder.Configuration["MongoDbSettings:ConnectionString"];

if (string.IsNullOrWhiteSpace(mongoConnectionString))
{
    throw new InvalidOperationException("A variável de ambiente MONGODB_CONNECTION_STRING não foi configurada.");
}

var databaseName = builder.Configuration["MongoDbSettings:DatabaseName"];

if (string.IsNullOrWhiteSpace(databaseName))
{
    throw new InvalidOperationException("O nome do banco de dados MongoDB não foi configurado.");
}

builder.Services.Configure<MongoDbSettings>(options =>
{
    builder.Configuration.GetSection("MongoDbSettings").Bind(options);
    options.ConnectionString = mongoConnectionString;
});

builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(mongoConnectionString));

builder.Services.AddSingleton<IMongoDatabase>(serviceProvider =>
{
    var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
    return mongoClient.GetDatabase(databaseName);
});

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddScoped<IRoupaRepository, RoupaRepository>();
builder.Services.AddScoped<IRoupaService, RoupaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
using System.Security.Cryptography.X509Certificates;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IDocumentStore>(ctx =>
{
    var store = new DocumentStore
    {
        Urls = new string[] { "http://127.0.0.1:8080/" },
        Database = "demo"
    };

    store.Initialize();

    return store;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

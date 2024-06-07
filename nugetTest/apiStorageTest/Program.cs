using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

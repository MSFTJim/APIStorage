using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/getJSON/{airportCode}", async (string airportCode) =>
{
    // retreive Azure Storage items from settings (in development, keys in secrets.json )
    string? connectionString = app.Configuration["AzureBlobStorage:ConnectionString"];
    string? containerName = app.Configuration["AzureBlobStorage:ContainerName"];
    string upperAirportCode = airportCode.ToUpper();
    

    // Create a BlobServiceClient
    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

    // Create a BlobContainerClient
    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);    
    
    // Create a BlobClient
    string jsonFileName = $"{airportCode.ToUpper()}SecurityTimes.json";
    BlobClient blobClient = containerClient.GetBlobClient(jsonFileName);

    // Download the blob content
    BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
    string blobContents = downloadResult.Content.ToString();
    
    app.Logger.LogInformation($"jsonFileName: {jsonFileName}");
    app.Logger.LogInformation($"blobContents: {blobContents}");

    return blobContents;

});



app.Run();

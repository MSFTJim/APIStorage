using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/SecurityTimes/{airportCode}", async (string airportCode) =>
{
    string jsonFileSuffix = "SecurityTimes.json";
    string blobContents = await GetBlobContents(airportCode, jsonFileSuffix);
    return blobContents;
});

app.MapGet("/WalkTimes/{airportCode}", async (string airportCode) =>
{
    string jsonFileSuffix = "WalkTimes.json";
    string blobContents = await GetBlobContents(airportCode, jsonFileSuffix);
    return blobContents;
});

async Task<string> GetBlobContents(string airportCode, string jsonFileSuffix)
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
    string jsonFileName = upperAirportCode + jsonFileSuffix;
    BlobClient blobClient = containerClient.GetBlobClient(jsonFileName);

    // Download the blob content
    BlobDownloadResult downloadResult = await blobClient.DownloadContentAsync();
    string blobContents = downloadResult.Content.ToString();

    //app.Logger.LogInformation($"jsonFileName: {jsonFileName}");
    app.Logger.LogInformation($"blobContents: {blobContents}");

    return blobContents;
}


app.Run();

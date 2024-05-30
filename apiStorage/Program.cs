var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/getJSON/{airportCode}", (string airportCode) =>

{
    app.Logger.LogInformation("getJSON endpoint called with airportCode: {airportCode}", airportCode);
    return $"Get JSON file for {airportCode}.";

});



app.Run();

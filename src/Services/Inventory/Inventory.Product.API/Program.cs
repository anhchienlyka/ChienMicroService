using Common.Logging;
using Inventory.Product.API.Extentions;
using Microsoft.AspNetCore.Routing;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Starting Inventory API up");

try
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    builder.Services.AddInfrastructureServices();
    builder.Services.AddConfigurationSettings(builder.Configuration);
    builder.Services.ConfigureMongoDbClient(builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapDefaultControllerRoute();
    app.Run();
}
catch (Exception ex)
{
    Log.Information($"Error Inventory API :{ex.Message}");
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
        throw;
    Log.Fatal(ex, "Unhanded exception");
}
finally
{
    Log.Information("Shut down Inventory API complete");
    Log.CloseAndFlush();
}
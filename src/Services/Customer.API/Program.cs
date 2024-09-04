using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Contracts.Common.Interfaces;
using Customer.API;
using Customer.API.Persistence;
using Serilog;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Customer.API.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Customer.API.Services.Interfaces;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
Log.Information($"Start {builder.Environment.ApplicationName} Api up");
try
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Host.AddAppConfigurations();

    builder.Services.ConfigureServices(builder.Configuration);

    var app = builder.Build();
    //map endpoint goi truc tiep service
    //app.MapGet(pattern: "/", handler: () => "Welcome to customer API!");
    //app.MapGet(pattern: "/api/customers",
    //           handler: async (string username, ICustomerService customerService)
    //           =>{
    //               var customer = await customerService.GetCustomerByUserName(username);
    //               return customer != null ? Results.Ok(customer) : Results.NotFound();
    //            });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseRouting();
    //app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        endpoints.MapDefaultControllerRoute();
    });

    //app.SeedCustomer();
    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if(type.Equals("StopTheHostException", StringComparison.Ordinal))
        throw;
    Log.Fatal(ex, "Unhanded exception");
}
finally
{
    Log.Information("Shut down Customer API complete");
    Log.CloseAndFlush();
}
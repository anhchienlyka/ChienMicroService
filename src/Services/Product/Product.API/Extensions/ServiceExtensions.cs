using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.API.Persistence;
using Serilog;
using System.Text.Json;

namespace Product.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureProductDbContext(configuration);
            return services;
        }

        private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
           
            var builder = new MySqlConnectionStringBuilder(connectionString);

            services.AddDbContext<ProductContext>(m => m.UseMySql(builder.ConnectionString,
                ServerVersion.AutoDetect(builder.ConnectionString), e =>
                {
                    e.MigrationsAssembly("Product.API");
                    e.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                }).LogTo(Console.WriteLine, LogLevel.Information)); 

            return services;
        }


        //private static IServiceCollection AddInfrastructureService(this IServiceCollection services)
        //{
        //    services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBase<,,>));
        //    services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        //    services.AddScoped<IProductRepository, ProductRepository>();

        //    return services;

        //}

        //private static void ConfigureHealthChecks(this IServiceCollection services)
        //{
        //    var databaseSettings = services.GetOptions<DatabaseSettings>(nameof(DatabaseSettings));
        //    Log.Information($"ConnectionString ConfigureHealthChecks: {JsonSerializer.Serialize(databaseSettings)}");
        //    services.AddHealthChecks()
        //        .AddMySql(databaseSettings.ConnectionString, "MySql Health", HealthStatus.Degraded);
        //}

        //public static void ConfigureSwagger(this IServiceCollection services)
        //{
        //    var configuration = services.GetOptions<ApiConfigurationSettings>(nameof(ApiConfigurationSettings));
        //    if (configuration == null || string.IsNullOrEmpty(configuration.IssuerUri) || string.IsNullOrEmpty(configuration.ApiName))
        //        throw new Exception("ApiConfigurationSettings is not configured!");
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new OpenApiInfo
        //        {
        //            Title = "Product API v1",
        //            Version = configuration.ApiVerion
        //        });

        //        c.AddSecurityDefinition(IdentityServerAuthenticationDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        //        {
        //            Type = SecuritySchemeType.OAuth2,
        //            Flows = new OpenApiOAuthFlows
        //            {
        //                Implicit = new OpenApiOAuthFlow
        //                {
        //                    AuthorizationUrl = new Uri($"{configuration.IdentityServerBaseUrl}/connect/authorize"),
        //                    Scopes = new Dictionary<string, string>()
        //                {
        //                    {"tedu_microservices_api.read", "Tedu Microservices API Read"},
        //                    {"tedu_microservices_api.write", "Tedu Microservices API Write"}
        //                }
        //                }
        //            }
        //        });

        //        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //        {
        //            {
        //                new OpenApiSecurityScheme
        //                {
        //                    Reference = new OpenApiReference
        //                    {
        //                        Type = ReferenceType.SecurityScheme,
        //                        Id = IdentityServerAuthenticationDefaults.AuthenticationScheme
        //                    },
        //                    Name = IdentityServerAuthenticationDefaults.AuthenticationScheme
        //                },
        //                new List<string>
        //                {
        //                    "tedu_microservices_api.read",
        //                    "tedu_microservices_api.write"
        //                }
        //            }
        //        });
        //    });
        //}
    }
}
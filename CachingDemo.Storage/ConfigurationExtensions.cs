using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CachingDemo.Storage;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var pgsql = configuration.GetConnectionString("Postgres");
        services.AddDbContext<AppDbContext>(o =>
            o.UseNpgsql(
                pgsql,
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        services.AddScoped<DbContext, AppDbContext>();
        services.AddScopedCached<IUserService, UserService, CachedUserService>();
        
        var redis = configuration.GetConnectionString("Redis");
        services.AddStackExchangeRedisCache(o => {
            o.Configuration = redis;
            o.InstanceName = "test-caching";
        });
        services.AddFusionCacheSystemTextJsonSerializer();
        services.AddFusionCacheStackExchangeRedisBackplane(o => {
            o.Configuration = redis;
        });
        services.AddFusionCache();
        
        return services;
    }

    public static IServiceCollection AddScopedCached<TInterface, TImpl, TCachedImpl>(
        this IServiceCollection serviceCollection)
        where TImpl : class 
        where TCachedImpl : class, TInterface
        where TInterface : class
    {
        serviceCollection.AddScoped<TImpl>();
        serviceCollection.AddScoped<TInterface, TCachedImpl>();
        return serviceCollection;
    }
}
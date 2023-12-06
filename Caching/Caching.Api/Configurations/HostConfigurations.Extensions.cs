using Caching.Api.Data;
using Caching.Application.Common.Identity.Services;
using Caching.Infrastructure.Common.Caching;
using Caching.Infrastructure.Common.Identity;
using Caching.Infrastructure.Common.Settings;
using Caching.Persistence.Caching;
using Caching.Persistence.DataContexts;
using Caching.Persistence.Repositories;
using Caching.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Caching.Api.Configurations;

public static partial class HostConfigurations
{
    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {

        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        //builder.Services.AddLazyCache();

        builder.Services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
                options.InstanceName = "Caching.Example";
            });
        
        //builder.Services.AddSingleton<ICacheBroker, LazyMemoryCacheBroker>();
        builder.Services.AddSingleton<ICacheBroker, RedisDistributedCacheBroker>();
        
        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IdentityDbContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        
        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }
    
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
    
    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();
        
        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
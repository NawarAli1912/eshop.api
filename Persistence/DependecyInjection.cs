using Domain.Categories.Abstraction.Repository;
using Domain.Customers.Abstraction.Repository;
using Domain.Orders.Abstraction.Repository;
using Domain.Products.Abstraction.Repository;
using Domain.SharedKernel.Abstraction;
using Domain.SharedKernel.Abstraction.ElasticTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nest;
using Persistence.Interceptors;
using Persistence.Repository;

namespace Persistence;

public static class DependecyInjection
{
    private const string ElasticSearchConfiguration = "ElasticConfiguration";

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {

        services.AddScoped<ProductRepository>();
        services.AddScoped<IProductRepository, CachedProductRepository>();

        services.AddScoped<OrderRepository>();
        services.AddScoped<IOrderRepository, CachedOrderRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.TryAdd(ServiceDescriptor.Singleton<IMemoryCache, MemoryCache>());

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            options.UseSqlServer(config.GetConnectionString("Default"))
                .AddInterceptors(interceptor!);
        });

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddElasticSearch(config);

        return services;
    }

    public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration[$"{ElasticSearchConfiguration}:Uri"]!;
        var defaultIndex = configuration[$"{ElasticSearchConfiguration}:index"]!;
        var settings = new ConnectionSettings(new Uri(url))
                            .PrettyJson()
                            .DefaultIndex(defaultIndex);
        AddDefaultMapping(settings);

        var client = new ElasticClient(settings);
        services.AddSingleton<IElasticClient>(client);

        CreateIndex(client, defaultIndex);
    }

    private static void AddDefaultMapping(ConnectionSettings settings)
    {
        settings.DefaultMappingFor<Product>(m => m
            .IndexName("products")
        );
    }

    private static void CreateIndex(IElasticClient client, string indexName)
    {
        client.Indices.Create(indexName, c => c
            .Map<Product>(m => m
                .AutoMap()
            )
         );
    }
}

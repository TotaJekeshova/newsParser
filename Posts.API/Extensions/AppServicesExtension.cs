using Posts.API.Repository;
using Posts.API.Repository.Interfaces;

namespace Posts.API.Extensions;

public static class AppServicesExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleRepository, ArticleRepository>();
        return services;
    }
}
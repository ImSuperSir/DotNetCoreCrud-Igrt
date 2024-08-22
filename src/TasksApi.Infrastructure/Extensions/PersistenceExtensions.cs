using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TaskApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TasksApi.Infrastructure.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            //options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
        });

        return services;

    }
}

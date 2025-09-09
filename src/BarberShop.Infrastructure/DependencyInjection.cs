using BarberShop.Domain.Repositories;
using BarberShop.Domain.Repositories.Revenues;
using BarberShop.Infrastructure.DataAccess;
using BarberShop.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Infrastructure;
public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IRevenuesWriteOnlyRepository, RevenuesRepository>();
        services.AddScoped<IRevenuesReadOnlyRepository, RevenuesRepository>();
        services.AddScoped<IRevenuesUpdateOnly, RevenuesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 43));

        services.AddDbContext<BarberShopDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }


}

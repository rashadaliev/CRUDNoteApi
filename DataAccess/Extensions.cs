using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<INoteRepository, NoteRepository>();
        serviceCollection.AddDbContext<AppContext>(option =>
        {
            option.UseNpgsql("Host=localhost;Database=noteDb;Username=postgres;Password=rashad");
        });
        return serviceCollection;
    }
}
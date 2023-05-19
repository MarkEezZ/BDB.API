using BDB.API.Repositories;

namespace BDB.API
{
    public static class ServiceExtentions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
                services.AddDbContext<AppDb>(opts =>
                    opts.UseSqlServer(configuration.GetConnectionString("SqlConnection"), b => b.MigrationsAssembly("BDB.API")));
    }
}

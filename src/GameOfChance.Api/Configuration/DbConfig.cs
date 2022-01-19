using GameOfChance.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfChance.Api.Configuration
{
    public static class DbConfig
    {
        public static void ConfigureSqlServer(this IServiceCollection services, IConfiguration Configuration)
        {
            // Configure Entityframework core with SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void ConfigureSqlite(this IServiceCollection services, IConfiguration Configuration)
        {
            // Configure Entityframework core with Sqlite
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnection"));
            });
        }
    }
}

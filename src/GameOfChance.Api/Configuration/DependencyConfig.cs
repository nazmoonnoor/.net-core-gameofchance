using GameOfChance.Client.Services;
using GameOfChance.Core;
using GameOfChance.Core.Repository;
using GameOfChance.Infrastructure;
using GameOfChance.SharedKernel;
using GameOfChance.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfChance.Api.Configuration
{
    public static class DependencyConfig
    {
        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration config)
        {
            //
            // Service layer
            services.AddSingleton<IGame, Game>();
            services.AddSingleton<ITokenBuilder, TokenBuilder>();
            services.AddSingleton<IRandomGenerator, DefaultRandom>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IGameManager, GameManager>();

            //
            //  Storage layer
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IBetRepository, BetRepository>();
        }

    }
}

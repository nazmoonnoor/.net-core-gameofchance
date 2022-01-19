using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GameOfChance.Api.Configuration
{
    public static class CacheConfig
    {
        public static readonly TimeSpan DefaultExpireTime = TimeSpan.FromMinutes(30);

        public static void AddCaching(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                // Use local distributed cache
                services.AddDistributedMemoryCache();
            }
            else
            {
                // Add support for distributed caching
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration["Cache:Redis:ConnectionString"];
                    options.InstanceName = configuration["Cache:Redis:InstanceName"];
                });
            }
        }
    }
}

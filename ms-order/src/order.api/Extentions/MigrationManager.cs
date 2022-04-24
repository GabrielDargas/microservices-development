using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Order.Api.Extentions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            return host;
        }
    }
}

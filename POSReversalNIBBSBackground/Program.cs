using Microsoft.EntityFrameworkCore;
using POSReversalNIBBSBackground.Data;
using POSReversalNIBBSBackground.Models;
using System;

namespace POSReversalNIBBSBackground
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    IConfiguration configuration = hostContext.Configuration;
                    AppSettings.ConnectionString = configuration.GetConnectionString("PosNibbsConnection");
                    var optionsBuilder = new DbContextOptionsBuilder<PosNibbsDbContext>();
                    optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
                    var optionsBuilder2 = new DbContextOptionsBuilder<PosNibbsLogDbContext>();
                    optionsBuilder2.UseSqlServer(AppSettings.ConnectionString);
                    services.AddScoped<PosNibbsDbContext>(db => new PosNibbsDbContext(optionsBuilder.Options));
                    services.AddScoped<PosNibbsLogDbContext>(db => new PosNibbsLogDbContext(optionsBuilder.Options));
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}
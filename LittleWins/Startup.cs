using System;
using System.Linq;
using LittleWins;
using LittleWins.Data;
using LittleWins.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace LittleWins
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // ------------------ default configuration initialise ------------------
            var serviceConfig = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(IConfiguration));
            if (serviceConfig != null)
            {
                _ = (IConfiguration) serviceConfig.ImplementationInstance;
            }


            builder.Services.AddLogging();

            // ------------------ TableStorageDb initialise ------------------
            ITableConfiguration tableConfig = new TableConfiguration
            {
                ConnectionString = Environment.GetEnvironmentVariable("TableStorage-ConnectionString"),
                TableName = Environment.GetEnvironmentVariable("TableStorage-TableName")
            };
    
            builder.Services.AddSingleton(tableConfig);
            builder.Services.AddSingleton<ITableDbContext, TableDbContext>();

        }
    }
}
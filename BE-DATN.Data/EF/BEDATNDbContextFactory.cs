using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BE_DATN.Data.EF
{
    public class BEDATNDbContextFactory : IDesignTimeDbContextFactory<BEDATNDbContext>
    {
        public BEDATNDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("BEDATNDb");

            var optionsBuilder = new DbContextOptionsBuilder<BEDATNDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BEDATNDbContext(optionsBuilder.Options);
        }
        
    }

}

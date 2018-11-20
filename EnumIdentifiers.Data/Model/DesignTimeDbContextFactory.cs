using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EnumIdentifiers.Data.Model
{
    public class DotNetFlixDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DotNetFlixDbContext>
    {
        public DotNetFlixDbContext CreateDbContext(string[] args)
        {
            var connectionString = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build().GetConnectionString("DotNetFlixDb");

            var options = new DbContextOptionsBuilder<DotNetFlixDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;
            return new DotNetFlixDbContext(options);
        }
    }
}
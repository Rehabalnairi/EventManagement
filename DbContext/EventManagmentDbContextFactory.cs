using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EventManagement.DbContext
{
    public class EventManagmentDbContextFactory : IDesignTimeDbContextFactory<EventManagmentDbContext>
    {
        public EventManagmentDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var configFile = Path.Combine(basePath, "appsettings.json");

            if (!File.Exists(configFile))
                throw new FileNotFoundException("appsettings.json not found", configFile);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<EventManagmentDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EventManagmentDbContext(optionsBuilder.Options);
        }
    }
}

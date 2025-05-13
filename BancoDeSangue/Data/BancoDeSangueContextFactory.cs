using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BancoDeSangue.Data
{
    public class BancoDeSangueContextFactory :IDesignTimeDbContextFactory<BancoDeSangueContext>
    {
        public BancoDeSangueContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BancoDeSangueContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new BancoDeSangueContext(builder.Options);
        }
    }
}

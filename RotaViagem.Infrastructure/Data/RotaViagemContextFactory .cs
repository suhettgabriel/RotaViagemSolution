using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace RotaViagem.Infrastructure.Data
{
    public class RotaViagemContextFactory : IDesignTimeDbContextFactory<RotaViagemContext>
    {
        public RotaViagemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RotaViagemContext>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"C:\Rota de Viagem\RotaViagem.Api\appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new RotaViagemContext(optionsBuilder.Options);
        }
    }
}

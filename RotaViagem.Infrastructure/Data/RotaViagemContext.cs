using Microsoft.EntityFrameworkCore;
using RotaViagem.Domain.Entities;

namespace RotaViagem.Infrastructure.Data
{
    public class RotaViagemContext : DbContext
    {
        public DbSet<Rota> Rotas { get; set; }

        public RotaViagemContext(DbContextOptions<RotaViagemContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rota>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Origem).IsRequired().HasMaxLength(3);
                entity.Property(r => r.Destino).IsRequired().HasMaxLength(3);
                entity.Property(r => r.Valor).IsRequired();
            });
        }
    }
}

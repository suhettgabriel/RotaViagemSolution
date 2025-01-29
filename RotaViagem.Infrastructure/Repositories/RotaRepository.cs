using RotaViagem.Domain.Entities;
using RotaViagem.Domain.Interfaces;
using RotaViagem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace RotaViagem.Infrastructure.Repositories
{
    public class RotaRepository : IRotaRepository
    {
        private readonly RotaViagemContext _context;

        public RotaRepository(RotaViagemContext context)
        {
            _context = context;
        }

        public void AdicionarRota(Rota rota)
        {
            _context.Rotas.Add(rota);
            _context.SaveChanges();
        }

        public IEnumerable<Rota> ObterTodasRotas()
        {
            return _context.Rotas.AsNoTracking().ToList();
        }

        public Rota? ObterRota(string origem, string destino)
        {
            return _context.Rotas
                .AsNoTracking()
                .FirstOrDefault(r => r.Origem == origem && r.Destino == destino);
        }
    }
}

using RotaViagem.Domain.Entities;

namespace RotaViagem.Domain.Interfaces
{
    public interface IRotaRepository
    {
        void AdicionarRota(Rota rota);
        IEnumerable<Rota> ObterTodasRotas();
        Rota? ObterRota(string origem, string destino);
    }
}

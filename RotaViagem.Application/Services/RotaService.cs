using RotaViagem.Domain.Entities;
using RotaViagem.Domain.Interfaces;

namespace RotaViagem.Application.Services
{
    public class RotaService
    {
        private readonly IRotaRepository _rotaRepository;

        public RotaService(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
        }

        public void AdicionarRota(Rota rota)
        {
            _rotaRepository.AdicionarRota(rota);
        }

        public List<(List<string> Rota, int Custo)> ObterMelhorRota(string origem, string destino)
        {
            var rotas = _rotaRepository.ObterTodasRotas().ToList();
            var resultados = new List<(List<string> Rota, int Custo)>();
            EncontrarRotas(origem, destino, rotas, new List<string> { origem }, 0, resultados);
            return resultados.OrderBy(r => r.Custo).ToList();
        }

        private void EncontrarRotas(string origem, string destino, List<Rota> rotas, List<string> rotaAtual, int custoAtual, List<(List<string> Rota, int Custo)> resultados)
        {
            if (origem == destino)
            {
                resultados.Add((new List<string>(rotaAtual), custoAtual));
                return;
            }

            var destinosPossiveis = rotas.Where(r => r.Origem == origem).ToList();
            foreach (var rota in destinosPossiveis)
            {
                if (!rotaAtual.Contains(rota.Destino))
                {
                    rotaAtual.Add(rota.Destino);
                    EncontrarRotas(rota.Destino, destino, rotas, rotaAtual, custoAtual + rota.Valor, resultados);
                    rotaAtual.RemoveAt(rotaAtual.Count - 1);
                }
            }
        }
    }
}
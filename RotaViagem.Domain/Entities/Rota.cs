namespace RotaViagem.Domain.Entities
{
    public class Rota
    {
        public int Id { get; private set; }
        public string Origem { get; private set; }
        public string Destino { get; private set; }
        public int Valor { get; private set; }

        public Rota(string origem, string destino, int valor)
        {
            if (string.IsNullOrWhiteSpace(origem) || string.IsNullOrWhiteSpace(destino))
                throw new ArgumentException("Origem e destino são obrigatórios.");
            if (valor <= 0)
                throw new ArgumentException("O valor da rota deve ser maior que zero.");

            Origem = origem;
            Destino = destino;
            Valor = valor;
        }
    }
}

using Moq;
using RotaViagem.Application.Services;
using RotaViagem.Domain.Entities;
using RotaViagem.Domain.Interfaces;

namespace RotaViagem.Tests
{
    public class RotaServiceTests
    {
        private readonly Mock<IRotaRepository> _rotaRepositoryMock;
        private readonly RotaService _rotaService;

        public RotaServiceTests()
        {
            _rotaRepositoryMock = new Mock<IRotaRepository>();
            _rotaService = new RotaService(_rotaRepositoryMock.Object);
        }

        [Fact]
        public void AdicionarRota_AddsRotaToRepository()
        {
            // Arrange
            var rota = new Rota("GRU", "BRC", 10);

            // Act
            _rotaService.AdicionarRota(rota);

            // Assert
            _rotaRepositoryMock.Verify(repo => repo.AdicionarRota(rota), Times.Once);
        }

        [Fact]
        public void AdicionarRota_DeveAdicionarRotaCorretamente()
        {
            var rota = new Rota("GRU", "CDG", 75);

            _rotaRepositoryMock.Setup(r => r.ObterRota("GRU", "CDG")).Returns((Rota)null!);
            _rotaRepositoryMock.Setup(r => r.AdicionarRota(It.IsAny<Rota>())).Verifiable();

            _rotaService.AdicionarRota(rota);

            _rotaRepositoryMock.Verify(r => r.AdicionarRota(It.IsAny<Rota>()), Times.Once);
        }

        [Fact]
        public void ObterMelhorRota_ReturnsBestRoute()
        {
            // Arrange
            var rotas = new List<Rota>
            {
                new Rota("GRU", "BRC", 10),
                new Rota("BRC", "SCL", 5),
                new Rota("GRU", "SCL", 20),
                new Rota("SCL", "ORL", 20),
                new Rota("ORL", "CDG", 5),
                new Rota("GRU", "CDG", 75)
            };

            _rotaRepositoryMock.Setup(repo => repo.ObterTodasRotas()).Returns(rotas);

            // Act
            var resultado = _rotaService.ObterMelhorRota("GRU", "CDG");

            // Assert
            var melhorRota = resultado.First();
            Assert.Equal(40, melhorRota.Custo);
            Assert.Equal(new List<string> { "GRU", "BRC", "SCL", "ORL", "CDG" }, melhorRota.Rota);
        }

        [Fact]
        public void ObterMelhorRota_NoRoutesFound_ReturnsEmptyList()
        {
            // Arrange
            _rotaRepositoryMock.Setup(repo => repo.ObterTodasRotas()).Returns(new List<Rota>());

            // Act
            var resultado = _rotaService.ObterMelhorRota("GRU", "CDG");

            // Assert
            Assert.Empty(resultado);
        }
    }
}
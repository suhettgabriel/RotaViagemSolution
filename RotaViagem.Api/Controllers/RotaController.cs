using Microsoft.AspNetCore.Mvc;
using RotaViagem.Application.Services;
using RotaViagem.Domain.Entities;

namespace RotaViagem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RotaController : ControllerBase
    {
        private readonly RotaService _rotaService;

        public RotaController(RotaService rotaService)
        {
            _rotaService = rotaService;
        }

        [HttpPost("adicionar")]
        public IActionResult AdicionarRota([FromBody] Rota rota)
        {
            _rotaService.AdicionarRota(rota);
            return Ok("Rota adicionada com sucesso!");
        }

        [HttpGet("melhorrota/{origem}/{destino}")]
        public IActionResult ObterMelhorRota(string origem, string destino)
        {
            var resultado = _rotaService.ObterMelhorRota(origem, destino);
            if (resultado.Count == 0)
            {
                return NotFound($"Nenhuma rota encontrada de {origem} para {destino}.");
            }

            var melhorRota = resultado.First();
            return Ok($"Melhor Rota: {string.Join(" - ", melhorRota.Rota)} ao custo de ${melhorRota.Custo}");
        }
    }
}
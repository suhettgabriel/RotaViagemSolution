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

        /// <summary>
        /// Cadastrar Rotas
        /// </summary>
        /// <param name="rota"></param>
        /// <returns></returns>
        [HttpPost("adicionar")]
        public IActionResult AdicionarRota([FromBody] Rota rota)
        {
            try
            {
                _rotaService.AdicionarRota(rota);
                return Ok("Rota adicionada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar rota: {ex.Message}");
            }
        }

        /// <summary>
        /// Buscar Melhor Rota
        /// </summary>
        /// <param name="origem"></param>
        /// <param name="destino"></param>
        /// <returns></returns>
        [HttpGet("melhorrota/{origem}/{destino}")]
        public IActionResult ObterMelhorRota(string origem, string destino)
        {
            try
            {
                var resultado = _rotaService.ObterMelhorRota(origem, destino);
                if (resultado.Count == 0)
                {
                    return NotFound($"Nenhuma rota encontrada de {origem} para {destino}.");
                }

                var melhorRota = resultado.First();
                return Ok($"Melhor Rota: {string.Join(" - ", melhorRota.Rota)} ao custo de ${melhorRota.Custo}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar a melhor rota: {ex.Message}");
            }
        }
    }
}

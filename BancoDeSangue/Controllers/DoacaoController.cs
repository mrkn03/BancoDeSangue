using BancoDeSangue.Data;
using BancoDeSangue.DTOs.NovaPasta;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories.Interfaces;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoacaoController : ControllerBase
    {
        private readonly IDoacaoRepository doacaoRepository;

        public DoacaoController(IDoacaoRepository doacaoRepository)
        {
            this.doacaoRepository = doacaoRepository;
        }

        [HttpPost("realizar-doacao")]
        public async Task<ActionResult> RealizarDoacao(DoacaoRequest request)
        {
            if (string.IsNullOrEmpty(request.Cpf))
            {
                return BadRequest("CPF do doador não pode ser nulo ou vazio.");
            }

            if (request.quantidadeML <= 0)
            {
                return BadRequest("A quantidade de sangue doada deve ser maior que zero.");
            }

            var doacao = await doacaoRepository.CriarDoacaoAsync(request.Cpf, request.quantidadeML);

            if (doacao == null)
            {
                return BadRequest("Erro ao criar a doação.");
            }

            return CreatedAtAction(nameof(RealizarDoacao), new { id = doacao.DoacaoId }, doacao);
        }

        [HttpGet("listar-doacoes")]
        public async Task<ActionResult> ListarDoacoes()
        {

            var doacoes = await doacaoRepository.ListarDoacoesAsync();

            return Ok(doacoes);
        }

        [HttpGet("recupera-doacoes")]
        public async Task<ActionResult> RecuperaDoacoes()
        {
            var doacoes = await doacaoRepository.RecuperaDoacoesAsync();

            return Ok(doacoes);
        }
    }
}
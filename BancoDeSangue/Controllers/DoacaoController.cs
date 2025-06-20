using BancoDeSangue.Data;
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

        [HttpPost]
        public async Task<ActionResult> RealizarDoacao(string cpfDoador, int quantidadeML)
        {
            if (string.IsNullOrEmpty(cpfDoador))
            {
                return BadRequest("CPF do doador não pode ser nulo ou vazio.");
            }

            if (quantidadeML <= 0)
            {
                return BadRequest("A quantidade de sangue doada deve ser maior que zero.");
            }

            await doacaoRepository.CriarDoacaoAsync(cpfDoador, quantidadeML);

            return Ok("Doação realizada com sucesso.");
        }



        [HttpGet("recupera-doacoes")]
        public async Task<ActionResult> RecuperaDoacoes()
        {

            var doacoes = await doacaoRepository.ListarDoacoesAsync();

            return Ok(doacoes);
        }

    }
}
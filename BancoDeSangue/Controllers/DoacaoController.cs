using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories.Interfaces;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoacaoController : ControllerBase
    {
        private readonly IDoacaoRepository doacaoRepository;

        public DoacaoController(IDoacaoRepository doacaoRepository)
        {
            this.doacaoRepository = doacaoRepository;
        }

        [HttpPost]
        public ActionResult RealizarDoacaoAsync(string cpfDoador, int quantidadeML)
        {
            if (string.IsNullOrEmpty(cpfDoador))
            {
                return BadRequest("CPF do doador não pode ser nulo ou vazio.");
            }

            if (quantidadeML <= 0)
            {
                return BadRequest("A quantidade de sangue doada deve ser maior que zero.");
            }

            

            doacaoRepository.CriarDoacao(cpfDoador, quantidadeML);

            return Ok("Doação realizada com sucesso.");
        }



        [HttpGet("recupera-doacoes")]
        public ActionResult RecuperaDoacoes()
        {

            var doacoes = doacaoRepository.ListarDoacoes();

            return Ok(doacoes);
        }

    }
}
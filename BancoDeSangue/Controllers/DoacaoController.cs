using BancoDeSangue.Data;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoacaoController : ControllerBase
    {
        private readonly BancoDeSangueContext _context;

        public DoacaoController(BancoDeSangueContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> AtualizaDoacaoAsync(string cpfDoador, int quantidadeML)
        {
            // Validações iniciais
            if (string.IsNullOrWhiteSpace(cpfDoador))
            {
                return BadRequest("O CPF do doador é obrigatório.");
            }

            if (quantidadeML <= 0)
            {
                return BadRequest("A quantidade de sangue doada deve ser maior que zero.");
            }

            // Busca o doador pelo CPF
            var doador = await _context.Doadores
                .FirstOrDefaultAsync(d => d.CpfDoador == cpfDoador);

            if (doador == null)
            {
                return NotFound("Doador não encontrado.");
            }

            // Verifica se o doador está apto a doar
            if (doador.UltimaDoacao != null && (DateTime.Now - doador.UltimaDoacao.Value).TotalDays < 90)
            {
                return BadRequest("O doador ainda não está apto para realizar uma nova doação.");
            }

            // Cria a nova doação
            var doacao = new Doacao
            {
                DoadorId = doador.DoadorId,
                QuantidadeML = quantidadeML,
                Data = DateTime.Now
            };

            // Atualiza a última doação do doador
            doador.UltimaDoacao = DateTime.Now;

            // Salva as alterações no banco de dados
            _context.Doacoes.Add(doacao);
            _context.Doadores.Update(doador);
            await _context.SaveChangesAsync();

            return Ok(doacao);
        }
    }
}

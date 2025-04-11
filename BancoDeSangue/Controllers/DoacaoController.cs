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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doacao>>> RecuperaDoacaoAsync() =>
            await _context.Doacoes.ToListAsync();

        [HttpGet("doador/{doadorId}")]
        public async Task<ActionResult<IEnumerable<Doacao>>> RecuperaPorCpfAsync(string doadorCpf) =>
            await _context.Doacoes.Where(doacao => doacao.Doador.Cpf == doadorCpf).ToListAsync();

        [HttpPost]
        public async Task<IActionResult> AtualizaDoacaoAsync(Doacao doacao)
        {
            var doador = await _context.Doadores.FindAsync(doacao.Doador.Cpf);
            if (doador == null) return NotFound("Doador não encontrado");

            _context.Doacoes.Add(doacao);
            doador.UltimaDoacao = doacao.Data;
            await _context.SaveChangesAsync();

            return Created("", doacao);
        }
    }
}

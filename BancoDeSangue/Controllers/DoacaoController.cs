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
        public async Task<ActionResult<IEnumerable<Doacao>>> Get() =>
            await _context.Doacoes.ToListAsync();

        [HttpGet("doador/{doadorId}")]
        public async Task<ActionResult<IEnumerable<Doacao>>> GetByDoador(Guid doadorId) =>
            await _context.Doacoes.Where(d => d.DoadorId == doadorId).ToListAsync();

        [HttpPost]
        public async Task<IActionResult> Post(Doacao doacao)
        {
            var doador = await _context.Doadores.FindAsync(doacao.DoadorId);
            if (doador == null) return NotFound("Doador não encontrado");

            _context.Doacoes.Add(doacao);
            doador.UltimaDoacao = doacao.Data;
            await _context.SaveChangesAsync();

            return Created("", doacao);
        }
    }
}

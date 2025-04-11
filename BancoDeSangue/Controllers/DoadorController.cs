using BancoDeSangue.Data;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoadorController : ControllerBase
    {
        private readonly BancoDeSangueContext _context;

        public DoadorController(BancoDeSangueContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doador>>> RecuperaDoadorAsync() =>
            await _context.Doadores.ToListAsync();

        [HttpGet("{cpf}")]
        public async Task<ActionResult<Doador>> RecuperaPorCpfAsync(string cpf)
        {
            var doador = await _context.Doadores.FirstOrDefaultAsync(d => d.Cpf == cpf);
            return doador is null ? NotFound() : Ok(doador);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaDoador(Doador doador)
        {
            _context.Doadores.Add(doador);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RecuperaPorCpfAsync), new { cpf = doador.Cpf }, doador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaDoadorAsync(string cpf, Doador doador)
        {
            if (cpf != doador.Cpf) return BadRequest();
            _context.Entry(doador).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluiDoadorAsync(string cpf)
        {
            var doador = await _context.Doadores.FindAsync(cpf);
            if (doador is null) return NotFound();
            _context.Doadores.Remove(doador);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

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
        public async Task<ActionResult<IEnumerable<Doador>>> Get() =>
            await _context.Doadores.ToListAsync();

        [HttpGet("{cpf}")]
        public async Task<ActionResult<Doador>> GetByCpf(string cpf)
        {
            var doador = await _context.Doadores.FirstOrDefaultAsync(d => d.Cpf == cpf);
            return doador is null ? NotFound() : Ok(doador);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Doador doador)
        {
            _context.Doadores.Add(doador);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByCpf), new { cpf = doador.Cpf }, doador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Doador doador)
        {
            if (id != doador.Id) return BadRequest();
            _context.Entry(doador).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var doador = await _context.Doadores.FindAsync(id);
            if (doador is null) return NotFound();
            _context.Doadores.Remove(doador);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

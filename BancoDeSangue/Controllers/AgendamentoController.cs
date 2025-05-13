using BancoDeSangue.Data;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly BancoDeSangueContext _context;

        public AgendamentoController(BancoDeSangueContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarAgendamento([FromBody] Agendamento agendamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var doador = await _context.Doadores.FindAsync(agendamento.DoadorId);
            if (doador == null)
                return NotFound("Doador não encontrado.");

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterAgendamentoPorId), new { id = agendamento.AgendamentoId }, agendamento);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agendamento>>> ListarAgendamentos()
        {
            var agendamentos = await _context.Agendamentos.Include(a => a.Doador).ToListAsync();

            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> ObterAgendamentoPorId(int id)
        {
            var agendamento = await _context.Agendamentos.Include(a => a.Doador)
                .FirstOrDefaultAsync(a => a.AgendamentoId == id);

            if (agendamento == null)
                return NotFound();

            return Ok(agendamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAgendamento(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
                return NotFound();

            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
      
    }
}
  
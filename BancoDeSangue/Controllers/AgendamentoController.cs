using BancoDeSangue.Data;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BancoDeSangue.Repository.Interfaces;
using BancoDeSangue.Repositories.Interfaces;

namespace BancoDeSangue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AgendamentoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async  Task<ActionResult<Agendamento>> CriarAgendamento([FromBody] Agendamento agendamento)
        {
           
             if(agendamento == null)
                return BadRequest("Agendamento não pode ser nulo.");

            await unitOfWork.AgendamentoRepository.CriarAsync(agendamento);
            
            await unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(ObterAgendamento), new { id = agendamento.AgendamentoId }, agendamento);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agendamento>>> ListarAgendamentos()
        {
            var agendamentos = await unitOfWork.AgendamentoRepository.ListarAgendamentoAsync();

            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> ObterAgendamento([FromRoute] int id)
        {
            var agendamento = await unitOfWork.AgendamentoRepository.RecuperarPorIdAsync(a => a.AgendamentoId == id);

            if (agendamento == null)
                return NotFound();

            return Ok(agendamento);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirAgendamento([FromRoute]int id)
        {
            var agendamento = await unitOfWork.AgendamentoRepository.RecuperarPorIdAsync(a => a.AgendamentoId == id);

            if (agendamento == null)
                return NotFound("Agendamento não encontrado");

            var agendamentoDeletado = await unitOfWork.AgendamentoRepository.ExcluirAsync(agendamento);
            await unitOfWork.CommitAsync();

            return Ok(agendamentoDeletado);
        }
      
    }
}
  
using BancoDeSangue.Data;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BancoDeSangue.Repository.Interfaces;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoRepository agendamentoRepository;

        public AgendamentoController(IAgendamentoRepository agendamentoRepository)
        {
        
            this.agendamentoRepository = agendamentoRepository;
        }

        [HttpPost]
        public ActionResult<Agendamento> CriarAgendamento([FromBody] Agendamento agendamento)
        {
           agendamentoRepository.CriarAgendamento(agendamento);

            return CreatedAtAction(nameof(ObterAgendamento), new { id = agendamento.AgendamentoId }, agendamento);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Agendamento>> ListarAgendamentos()
        {
            var agendamentos = agendamentoRepository.ListarAgendamentos();

            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public ActionResult<Agendamento> ObterAgendamento(int id)
        {
            var agendamento = agendamentoRepository.ObterAgendamento(id);

            if (agendamento == null)
                return NotFound();

            return Ok(agendamento);
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirAgendamento(int id)
        {
            var agendamento = agendamentoRepository.ObterAgendamento(id);

            if (agendamento == null)
                return NotFound();

            var agendamentoDeletado = agendamentoRepository.DeletarAgendamento(id);

            return Ok(agendamentoDeletado);
        }
      
    }
}
  
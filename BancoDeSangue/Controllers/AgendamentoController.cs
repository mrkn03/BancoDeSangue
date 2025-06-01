using BancoDeSangue.Data;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BancoDeSangue.Repository.Interfaces;
using BancoDeSangue.Repositories.Interfaces;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AgendamentoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult<Agendamento> CriarAgendamento([FromBody] Agendamento agendamento)
        {
           
             if(agendamento == null)
                return BadRequest("Agendamento não pode ser nulo.");

            unitOfWork.AgendamentoRepository.Criar(agendamento);
            
            unitOfWork.Commit();

            return CreatedAtAction(nameof(ObterAgendamento), new { id = agendamento.AgendamentoId }, agendamento);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Agendamento>> ListarAgendamentos()
        {
            var agendamentos = unitOfWork.AgendamentoRepository.Listar();

            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public ActionResult<Agendamento> ObterAgendamento(int id)
        {
            var agendamento = unitOfWork.AgendamentoRepository.ObterPorId(a => a.AgendamentoId == id);

            if (agendamento == null)
                return NotFound();

            return Ok(agendamento);
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirAgendamento(int id)
        {
            var agendamento = unitOfWork.AgendamentoRepository.ObterPorId(a => a.AgendamentoId == id);

            if (agendamento == null)
                return NotFound("Agendamento não encontrado");

            var agendamentoDeletado = unitOfWork.AgendamentoRepository.Excluir(agendamento);
            unitOfWork.Commit();

            return Ok(agendamentoDeletado);
        }
      
    }
}
  
using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories.Interfaces;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoadorController : ControllerBase
    {
        
        private readonly IUnitOfWork unitOfWork;

        public DoadorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doador>>> ListarDoadores()
        {
            var doadores = await unitOfWork.DoadorRepository.ListarAsync();

            return Ok(doadores);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<Doador>> RecuperarDoador(string cpf)
        {
            var doador = await unitOfWork.DoadorRepository.RecuperarPorIdAsync(d => d.Cpf == cpf);

            if (doador is null)
            {
                return NotFound("Doador não encontrado.");
            }


            return doador;
        }

        [HttpPost]
        public async Task<ActionResult> AdicionaDoador(Doador doador)
        {
            await unitOfWork.DoadorRepository.CriarAsync(doador);
            await unitOfWork.CommitAsync();

            return Ok(doador);
        }

        [HttpPut("{cpf}")]
        public async Task<ActionResult> AtualizaDoador(string cpf, Doador doador)
        {
            if(cpf != doador.Cpf)
            {
                return BadRequest("Dados Invalidos");
            }

            await unitOfWork.DoadorRepository.AtualizarAsync(doador);
            await unitOfWork.CommitAsync();

            return Ok(doador);
        }

        [HttpDelete("{cpf}")]
        public async Task<ActionResult> ExcluiDoador(string cpf)
        {
            var doador = await unitOfWork.DoadorRepository.RecuperarPorIdAsync(d => d.Cpf == cpf);

            if (doador is null)
            {
                return NotFound("Doador nao encontrado");
            }

            var doadorDeletado = await unitOfWork.DoadorRepository.ExcluirAsync(doador);
            await unitOfWork.CommitAsync();

            return Ok(doadorDeletado);
        }
    }
}

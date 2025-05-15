using BancoDeSangue.Data;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar as operações relacionadas aos doadores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DoadorController : ControllerBase
    {
        private readonly BancoDeSangueContext context;

        public DoadorController(BancoDeSangueContext context) => this.context = context;

        /// <summary>
        /// Recupera todos os doadores cadastrados.
        /// </summary>
        /// <returns>Uma lista de doadores.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doador>>> RecuperaDoadorAsync() =>
                await context.Doadores.ToListAsync();

        /// <summary>
        /// Recupera um doador pelo CPF.
        /// </summary>
        /// <param name="cpf">CPF do doador a ser recuperado.</param>
        /// <returns>O doador correspondente ao CPF informado.</returns>
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Doador>> RecuperaPorCpfAsync(string cpf)
        {
            var doador = await context.Doadores.FirstOrDefaultAsync(d => d.Cpf == cpf);

            return doador is null ? NotFound() : Ok(doador);
        }

        /// <summary>
        /// Adiciona um novo doador ao sistema.
        /// </summary>
        /// <param name="doador">Objeto doador a ser adicionado.</param>
        /// <returns>O doador criado com a rota para acessá-lo.</returns>
        [HttpPost]
        public async Task<IActionResult> AdicionaDoador([FromBody] Doador doador)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                context.Doadores.Add(doador);

                await context.SaveChangesAsync();

                return Ok(doador);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar doador: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza as informações de um doador existente.
        /// </summary>
        /// <param name="cpf">CPF do doador a ser atualizado.</param>
        /// <param name="doador">Objeto doador com as informações atualizadas.</param>
        /// <returns>Um status HTTP indicando o resultado da operação.</returns>
        [HttpPut("{cpf}")]
        public async Task<IActionResult> AtualizaDoadorAsync(string cpf, [FromBody] Doador doador)
        {
            if (cpf != doador.Cpf)
            {
                return NotFound("O CPF informado não corresponde ao doador.");
            }

            var doadorExistente = await context.Doadores.FirstOrDefaultAsync(d => d.Cpf == cpf);

            if (doadorExistente == null)
            {
                return NotFound("Doador não encontrado.");
            }

            try
            {
                context.Entry(doador).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return Ok(doador);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar doador: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui um doador do sistema.
        /// </summary>
        /// <param name="cpf">CPF do doador a ser excluído.</param>
        /// <returns>Um status HTTP indicando o resultado da operação.</returns>
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> ExcluiDoadorAsync(string cpf)
        {
            var doador = await context.Doadores.FirstOrDefaultAsync(d => d.Cpf == cpf);
            if (doador == null)
            {
                return NotFound("Doador não encontrado.");
            }

            try
            {
                context.Doadores.Remove(doador);
               
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir doador: {ex.Message}");
            }
        }
    }
}

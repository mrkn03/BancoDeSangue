using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories.Interfaces;
using BancoDeSangue.Repository.Interfaces;
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
        
        private readonly IUnitOfWork unitOfWork;

        public DoadorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Recupera todos os doadores cadastrados.
        /// </summary>
        /// <returns>Uma lista de doadores.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Doador>> ListarDoadores()
        {
            var doadores = unitOfWork.DoadorRepository.Listar();

            return Ok(doadores);
        }

        /// <summary>
        /// Recupera um doador pelo CPF.
        /// </summary>
        /// <param name="cpf">CPF do doador a ser recuperado.</param>
        /// <returns>O doador correspondente ao CPF informado.</returns>
        [HttpGet("{cpf}")]
        public ActionResult<Doador> RecuperarDoador(string cpf)
        {
            var doador = unitOfWork.DoadorRepository.ObterPorId(d => d.Cpf == cpf);

            if (doador is null)
            {
                return NotFound("Doador não encontrado.");
            }


            return (doador);
        }

        /// <summary>
        /// Adiciona um novo doador ao sistema.
        /// </summary>
        /// <param name="doador">Objeto doador a ser adicionado.</param>
        /// <returns>O doador criado com a rota para acessá-lo.</returns>
        [HttpPost]
        public ActionResult AdicionaDoador(Doador doador)
        {
            unitOfWork.DoadorRepository.Criar(doador);
            unitOfWork.Commit();

            return Ok(doador);
        }

        /// <summary>
        /// Atualiza as informações de um doador existente.
        /// </summary>
        /// <param name="cpf">CPF do doador a ser atualizado.</param>
        /// <param name="doador">Objeto doador com as informações atualizadas.</param>
        /// <returns>Um status HTTP indicando o resultado da operação.</returns>
        [HttpPut("{cpf}")]
        public ActionResult AtualizaDoador(string cpf, Doador doador)
        {
            if(cpf != doador.Cpf)
            {
                return BadRequest("Dados Invalidos");
            }

            unitOfWork.DoadorRepository.Atualizar(doador);
            unitOfWork.Commit();

            return Ok(doador);
        }

        /// <summary>
        /// Exclui um doador do sistema.
        /// </summary>
        /// <param name="cpf">CPF do doador a ser excluído.</param>
        /// <returns>Um status HTTP indicando o resultado da operação.</returns>
        [HttpDelete("{cpf}")]
        public  ActionResult ExcluiDoador(string cpf)
        {
            var doador = unitOfWork.DoadorRepository.ObterPorId(d => d.Cpf == cpf);

            if (doador is null)
            {
                return NotFound("Doador nao encontrado");
            }

            var doadorDeletado = unitOfWork.DoadorRepository.Excluir(doador);
            unitOfWork.Commit();

            return Ok(doadorDeletado);
        }
    }
}

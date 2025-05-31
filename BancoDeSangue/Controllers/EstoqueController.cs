using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueDeSangueRepository estoqueRepository;

        public EstoqueController(IEstoqueDeSangueRepository estoqueRepository)
        {
            this.estoqueRepository = estoqueRepository;
        }

        [HttpGet]
        public ActionResult<EstoqueDeSangue> GetEstoque()
        {

            var estoque = estoqueRepository.RecuperaEstoque();

            if (estoque == null)
            {
                return NotFound();
            }
            return Ok(estoque);
        }
    }
}

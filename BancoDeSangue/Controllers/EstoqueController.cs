using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories.Interfaces;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public EstoqueController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<EstoqueDeSangue> GetEstoque()
        {

            var estoque = unitOfWork.EstoqueDeSangueRepository.ObterPorId(e => e.Id == 1);

            if (estoque == null)
            {
                return NotFound();
            }

            return Ok(estoque);
        }
    }
}

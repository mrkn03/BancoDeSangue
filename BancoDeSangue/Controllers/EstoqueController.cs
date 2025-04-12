using BancoDeSangue.Data;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly BancoDeSangueContext _context;
        public EstoqueController(BancoDeSangueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<EstoqueDeSangue> GetEstoque()
        {

            var estoque = _context.Estoques.FirstOrDefault();
            if (estoque == null)
            {
                return NotFound();
            }
            return Ok(estoque);
        }
    }
}

using BancoDeSangue.Data;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoacaoController : ControllerBase
    {
        private readonly BancoDeSangueContext _context;

        public DoacaoController(BancoDeSangueContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> RealizarDoacaoAsync(string cpfDoador, int quantidadeML)
        {
            if (string.IsNullOrWhiteSpace(cpfDoador))
                return BadRequest("O CPF do doador é obrigatório.");

            if (quantidadeML <= 0)
                return BadRequest("A quantidade de sangue doada deve ser maior que zero.");

            var doador = await _context.Doadores
                .FirstOrDefaultAsync(d => d.CpfDoador == cpfDoador); // Ajuste aqui: era CpfDoador

            if (doador == null)
                return NotFound("Doador não encontrado.");

            if (doador.UltimaDoacao != null && (DateTime.Now - doador.UltimaDoacao.Value).TotalDays < 90)
                return BadRequest("O doador ainda não está apto para realizar uma nova doação.");

            // Cria a doação
            var doacao = new Doacao
            {
                DoadorId = doador.DoadorId,
                QuantidadeML = quantidadeML,
                Data = DateTime.Now
            };

            doador.UltimaDoacao = DateTime.Now;

            // Atualiza o estoque
            var estoque = await _context.Estoques.FirstOrDefaultAsync();
            if (estoque == null)
                return NotFound("Estoque de sangue não encontrado.");

            switch (doador.TipoSanguineo.ToUpper())
            {
                case "O+":
                    estoque.TotalOPositivo += quantidadeML;
                    break;
                case "O-":
                    estoque.TotalONegativo += quantidadeML;
                    break;
                case "A+":
                    estoque.TotalAPositivo += quantidadeML;
                    break;
                case "A-":
                    estoque.TotalANegativo += quantidadeML;
                    break;
                case "B+":
                    estoque.TotalBPositivo += quantidadeML;
                    break;
                case "B-":
                    estoque.TotalBNegativo += quantidadeML;
                    break;
                case "AB+":
                    estoque.TotalABPositivo += quantidadeML;
                    break;
                case "AB-":
                    estoque.TotalABNegativo += quantidadeML;
                    break;
                default:
                    return BadRequest("Tipo sanguíneo inválido.");
            }

            estoque.TotalEstoque += quantidadeML;

            // Persiste todas as alterações
            _context.Doacoes.Add(doacao);
            _context.Doadores.Update(doador);
            _context.Estoques.Update(estoque);
            await _context.SaveChangesAsync();

            return Ok(doacao);
        }

    }
}

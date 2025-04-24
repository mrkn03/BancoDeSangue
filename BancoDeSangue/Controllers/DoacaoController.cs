using BancoDeSangue.Data;
using BancoDeSangue.Models;
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
                .FirstOrDefaultAsync(d => d.CpfDoador == cpfDoador);

            if (doador == null)
                return NotFound("Doador não encontrado.");

            if (doador.UltimaDoacao != null && (DateTime.Now - doador.UltimaDoacao.Value).TotalDays < 90)
                return BadRequest("O doador ainda não está apto para realizar uma nova doação.");

            var doacao = new Doacao
            {
                DoadorId = doador.DoadorId,
                QuantidadeML = quantidadeML,
                Data = DateTime.Now
            };

            doador.UltimaDoacao = DateTime.Now;

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

            _context.Doacoes.Add(doacao);

            _context.Doadores.Update(doador);

            _context.Estoques.Update(estoque);

            await _context.SaveChangesAsync();

            return Ok(doacao);
        }

        //[HttpGet("doacoes-por-periodo")]
        //public async Task<ActionResult> GetDoacoesPorPeriodo(int? ano = null, int? mes = null)
        //{
        //    var query = _context.Doacoes.AsQueryable();

        //    if (ano.HasValue)
        //    {
        //        query = query.Where(d => d.Data.Year == ano.Value);
        //    }

        //    if (mes.HasValue)
        //    {
        //        query = query.Where(d => d.Data.Month == mes.Value);
        //    }

        //    var doacoesPorPeriodo = await query
        //        .GroupBy(d => new { d.Data.Year, d.Data.Month })
        //        .Select(group => new
        //        {
        //            Periodo = $"{group.Key.Month}/{group.Key.Year}",
        //            TotalDoacoes = group.Count()
        //        })
        //        .ToListAsync();

        //    if (doacoesPorPeriodo == null || !doacoesPorPeriodo.Any())
        //    {
        //        return NotFound("Não há doações registradas.");
        //    }

        //    return Ok(doacoesPorPeriodo);
        //}

        [HttpGet("recupera-doacoes")]
        public async Task<ActionResult> RecuperaDoacoes()
        {
            var dataAtual = DateTime.Now;
            var dataInicio = dataAtual.AddMonths(-12);

           
            var mesesDoAno = Enumerable.Range(1, 13)
                .Select(i => dataInicio.AddMonths(i))
                .Select(data => new
                {
                    Ano = data.Year,
                    Mes = data.Month,
                    MesAno = data.ToString("MMM yyyy"),
                    TotalDoacoes = 0
                })
                .ToList();

            var doacoesPorPeriodo = await _context.Doacoes
               .Where(d => d.Data >= dataInicio && d.Data <= dataAtual)
               .GroupBy(d => new { d.Data.Year, d.Data.Month })
               .Select(group => new
               {
                   Ano = group.Key.Year,
                   Mes = group.Key.Month,
                   TotalDoacoes = group.Count()
               })
               .ToListAsync();


            var resultadoFinal = mesesDoAno
                .GroupJoin(
                    doacoesPorPeriodo,
                    mes => new { mes.Ano, mes.Mes },
                    doacao => new { doacao.Ano, doacao.Mes },
                    (mes, doacoes) => new
                    {
                        mes.MesAno,
                        TotalDoacoes = doacoes.Sum(d => d.TotalDoacoes)
                    }
                )
                .OrderBy(result => DateTime.ParseExact(result.MesAno, "MMM yyyy", null))
                .ToList();

            return Ok(resultadoFinal);
        }

    }
}
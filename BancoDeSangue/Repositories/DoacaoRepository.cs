using BancoDeSangue.Data;
using BancoDeSangue.DTOs;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Repository
{
    public class DoacaoRepository(BancoDeSangueContext context) : Repository<Doacao>(context), IDoacaoRepository
    {
        public Doacao CriarDoacao(string cpf, int quantidadeML)
        {

            var doador = context.Doadores
                .FirstOrDefault(d => d.Cpf == cpf) ?? throw new ArgumentException("Doador não encontrado.");

            if (doador.UltimaDoacao != null && (DateTime.Now - doador.UltimaDoacao.Value).TotalDays < 90)
            {
                throw new InvalidOperationException("O doador ainda não está apto para realizar uma nova doação.");
            }

            var doacao = new Doacao
            {
                DoadorId = doador.Id,
                QuantidadeML = quantidadeML,
                Data = DateTime.Now
            };

            doador.UltimaDoacao = DateTime.Now;

            context.Doacoes.Add(doacao);
            context.SaveChanges();

            var estoque = context.Estoques.FirstOrDefault();

            if (estoque == null)
            {
                throw new InvalidOperationException("Estoque de sangue não encontrado.");
            }

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
                    throw new InvalidOperationException("Tipo sanguíneo inválido.");
            }

            context.SaveChanges();
            return doacao;

        }

        public IEnumerable<DoacaoDTO> ListarDoacoes()
        {
            var dataAtual = DateTime.Now;
            var dataInicio = dataAtual.AddMonths(-12);

            var mesesDoAno = Enumerable.Range(1, 13)
                .Select(i => dataInicio.AddMonths(i))
                .Select(data => new
                {
                    Ano = data.Year,
                    Mes = data.Month,
                    MesAno = data.ToString("MM yyyy"),
                    TotalDoacoes = 0
                })
                .ToList();

            var doacoesPorPeriodo = context.Doacoes
                .Where(d => d.Data >= dataInicio && d.Data <= dataAtual)
                .GroupBy(d => new { d.Data.Year, d.Data.Month })
                .Select(group => new
                {
                    Ano = group.Key.Year,
                    Mes = group.Key.Month,
                    TotalDoacoes = group.Count()
                })
                .ToList();

            var resultadoFinal = mesesDoAno
                .GroupJoin(
                    doacoesPorPeriodo,
                    mes => new { mes.Ano, mes.Mes },
                    doacao => new { doacao.Ano, doacao.Mes },
                    (mes, doacoes) => new DoacaoDTO
                    {
                        MesAno = mes.MesAno,
                        TotalDoacoes = doacoes.Sum(d => d.TotalDoacoes)
                    }
                )

                .OrderBy(result => DateTime.ParseExact(result.MesAno, "MM yyyy", null))
                .ToList();

            return resultadoFinal;
        }
    }
}

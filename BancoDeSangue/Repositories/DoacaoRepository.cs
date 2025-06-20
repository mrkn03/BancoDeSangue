using BancoDeSangue.Data;
using BancoDeSangue.DTOs;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories;
using BancoDeSangue.Repositories.Interfaces;
using BancoDeSangue.Repository.Interfaces;

namespace BancoDeSangue.Repository
{
    public class DoacaoRepository : Repository<Doacao>, IDoacaoRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly BancoDeSangueContext context;

        public DoacaoRepository(BancoDeSangueContext context) : base(context)
        {
        }

        public DoacaoRepository(IUnitOfWork unitOfWork, BancoDeSangueContext context) : base(context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        public async Task<Doacao> CriarDoacaoAsync(string cpf, int quantidadeML)
        {
            var doador = await unitOfWork.DoadorRepository
                .RecuperarPorIdAsync(d => d.Cpf == cpf)
                ?? throw new ArgumentException("Doador não encontrado.");

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

            await unitOfWork.DoacaoRepository.CriarAsync(doacao);

            await unitOfWork.CommitAsync();

            var estoque = await unitOfWork.EstoqueDeSangueRepository.RecuperarAsync() ?? throw new NullReferenceException("Estoque não encontrado");

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

            await unitOfWork.CommitAsync();
            return doacao;
        }

        public async Task<IEnumerable<DoacaoDTO>> ListarDoacoesAsync()
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

            var doacoes = await unitOfWork.DoacaoRepository.ListarAsync();

            var doacoesPorPeriodo = doacoes
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

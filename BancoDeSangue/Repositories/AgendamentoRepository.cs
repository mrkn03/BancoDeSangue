using BancoDeSangue.Data;
using BancoDeSangue.DTOs;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories;
using BancoDeSangue.Repositories.Interfaces;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Repository
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly BancoDeSangueContext context;

        public AgendamentoRepository(BancoDeSangueContext context) : base(context)
        {
            this.context = context;
        }

        public AgendamentoRepository(IUnitOfWork unitOfWork, BancoDeSangueContext context) : base(context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        public async Task<IEnumerable<AgendamentoDTO>> ListarAgendamentoAsync()
        {
            var agendamentos = await context.Agendamentos
                .Include(a => a.Doador)
                .AsNoTracking()
                .ToListAsync();

            var agendamentoDTOs = agendamentos.Select(a => new AgendamentoDTO
            {
                AgendamentoId = a.AgendamentoId,
                DoadorId = a.DoadorId,
                NomeDoador = a.Doador.Nome,
                Data = a.Data,
                LocalColeta = a.LocalColeta,
                Observacoes = a.Observacoes
            }).ToList();

            return agendamentoDTOs;
        }
    }
}

using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Repository
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly BancoDeSangueContext context;

        public AgendamentoRepository(BancoDeSangueContext context)
        {
            this.context = context;
            
        }

        public Agendamento CriarAgendamento(Agendamento agendamento)
        {
            context.Agendamentos.Add(agendamento);
            context.SaveChanges();
       

            return agendamento;
        }

        public Agendamento AtualizarAgendamento(Agendamento agendamento)
        {
            if (agendamento is null)
            {                
                throw new ArgumentNullException("Agendamento não pode ser nulo.");
            }

            context.Entry(agendamento).State = EntityState.Modified;
            context.SaveChanges();
            
            return agendamento;
        }

        public Agendamento DeletarAgendamento(int id)
        {
            var agendamento = context.Agendamentos.Find(id);

            if (agendamento is null)
            {                
                throw new ArgumentNullException("Agendamento não encontrado.");
            }

            context.Agendamentos.Remove(agendamento);
            context.SaveChanges();

            return agendamento;
        }

        public IEnumerable<Agendamento> ListarAgendamentos()
        {
            var agendamentos = context.Agendamentos.Include(a => a.Doador).ToList();


            return agendamentos;
        }

        public Agendamento ObterAgendamento(int id)
        {
            var agendamento = context.Agendamentos.FirstOrDefault(a => a.AgendamentoId == id);

            if (agendamento is null)
            {                
                throw new ArgumentNullException("Agendamento não encontrado.");
            }

            return agendamento;
        }

    }
}

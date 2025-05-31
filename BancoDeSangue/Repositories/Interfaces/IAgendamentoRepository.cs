using BancoDeSangue.Models;

namespace BancoDeSangue.Repository.Interfaces
{
    public interface IAgendamentoRepository
    {
        Agendamento CriarAgendamento(Agendamento agendamento);
        Agendamento AtualizarAgendamento(Agendamento agendamento);
        Agendamento ObterAgendamento(int id);
        Agendamento DeletarAgendamento(int id);
        IEnumerable<Agendamento> ListarAgendamentos();
    }
}

using BancoDeSangue.DTOs;
using BancoDeSangue.Models;

namespace BancoDeSangue.Repository.Interfaces
{
    public interface IAgendamentoRepository : IRepository<Agendamento>
    {
        Task<IEnumerable<AgendamentoDTO>> ListarAgendamentoAsync();
    }
}

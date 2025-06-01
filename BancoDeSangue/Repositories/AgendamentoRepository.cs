using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Repository
{
    public class AgendamentoRepository(BancoDeSangueContext context) : Repository<Agendamento>(context), IAgendamentoRepository
    {
    }
}

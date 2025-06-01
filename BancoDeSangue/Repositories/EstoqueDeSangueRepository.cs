using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories;
using BancoDeSangue.Repository.Interfaces;

namespace BancoDeSangue.Repository
{
    public class EstoqueDeSangueRepository(BancoDeSangueContext context) : Repository<EstoqueDeSangue>(context), IEstoqueDeSangueRepository
    {
    }
}

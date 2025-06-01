using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories;
using BancoDeSangue.Repository.Interfaces;

namespace BancoDeSangue.Repository
{
    public class DoadorRepository(BancoDeSangueContext context) : Repository<Doador>(context), IDoadorRepository
    {
    }
}

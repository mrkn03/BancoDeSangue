using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repository.Interfaces;

namespace BancoDeSangue.Repository
{
    public class EstoqueDeSangueRepository : IEstoqueDeSangueRepository
    {
        private readonly BancoDeSangueContext context;

        public EstoqueDeSangueRepository(BancoDeSangueContext context)
        {
            this.context = context;
        }

        public EstoqueDeSangue RecuperaEstoque()
        {
            var estoque = context.Estoques.FirstOrDefault();

            return estoque;
        }
    }
}

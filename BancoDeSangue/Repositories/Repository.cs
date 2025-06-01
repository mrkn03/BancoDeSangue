using System.Linq.Expressions;
using BancoDeSangue.Data;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly BancoDeSangueContext context;

        public Repository(BancoDeSangueContext context)
        {
            this.context = context;
        }

        public T Criar(T entidade)
        {
            context.Set<T>().Add(entidade);
            
            return entidade;
        }

        public T Atualizar(T entidade)
        {
            context.Set<T>().Update(entidade);
            
            return entidade;
        }

        public T Excluir(T entidade)
        {
            context.Set<T>().Remove(entidade);

            return entidade;
        }

        public IEnumerable<T> Listar()
        {
            return context.Set<T>().AsNoTracking().ToList();
        }

        public T? ObterPorId(Expression<Func<T, bool>> expressao)
        {
            return context.Set<T>().FirstOrDefault(expressao);
        }
    }
}

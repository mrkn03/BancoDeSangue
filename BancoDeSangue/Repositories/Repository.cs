using System.Linq.Expressions;
using BancoDeSangue.Data;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoDeSangue.Repositories
{
    public class Repository<T>(BancoDeSangueContext context) : IRepository<T> where T : class
    {

        private readonly BancoDeSangueContext context = context;

        public async Task<T> CriarAsync(T entidade)
        {
            await context.Set<T>().AddAsync(entidade);
            
            return entidade;
        }

        public Task<T> AtualizarAsync(T entidade)
        {
            context.Set<T>().Update(entidade);
            
            return Task.FromResult(entidade);
        }

        public Task<T> ExcluirAsync(T entidade)
        {
            context.Set<T>().Remove(entidade);

            return Task.FromResult(entidade);
        }

        public async Task<IEnumerable<T>> ListarAsync()
        {
            return await context.Set<T>()
                          .AsNoTracking()
                          .ToListAsync();
        }

        public async Task<T?> RecuperarAsync()
        {
            return await context.Set<T>().FirstOrDefaultAsync();
        }

        public async Task<T?> RecuperarPorIdAsync(Expression<Func<T, bool>> expressao)
        {
            return await context.Set<T>().FirstOrDefaultAsync(expressao);
        }
    }
}

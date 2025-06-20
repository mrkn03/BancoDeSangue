using System.Linq.Expressions;
using BancoDeSangue.Models;

namespace BancoDeSangue.Repository.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> CriarAsync(T entidade);
        Task<T> AtualizarAsync(T entidade);
        Task<T> ExcluirAsync(T entidade);
        Task<T?> RecuperarAsync();
        Task<T?> RecuperarPorIdAsync(Expression<Func<T, bool>> expressao);
        Task<IEnumerable<T>> ListarAsync();
    }
}

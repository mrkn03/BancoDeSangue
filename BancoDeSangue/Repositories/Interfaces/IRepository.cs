using System.Linq.Expressions;
using BancoDeSangue.Models;

namespace BancoDeSangue.Repository.Interfaces
{
    public interface IRepository<T>
    {
        T Criar(T entidade);
        T Atualizar(T entidade);
        T Excluir(T entidade);
        T? ObterPorId(Expression<Func<T, bool>> expressao);
        IEnumerable<T> Listar();
    }
}

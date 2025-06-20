using BancoDeSangue.Models;
using BancoDeSangue.Repository.Interfaces;

namespace BancoDeSangue.Repositories.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> RecuperarUsuarioPorEmailAsync(string email);
    }
}

using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories.Interfaces;

namespace BancoDeSangue.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly IUnitOfWork unitOfWork;

        public UsuarioRepository(BancoDeSangueContext context, IUnitOfWork unitOfWork) : base(context)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Usuario> RecuperarUsuarioPorEmailAsync(string email)
        {
            var usuario = await unitOfWork.UsuarioRepository
                .RecuperarPorIdAsync(u => u.Email == email);

            return usuario;
        }
    }
}

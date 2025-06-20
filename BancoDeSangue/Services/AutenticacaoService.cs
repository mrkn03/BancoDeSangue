using BancoDeSangue.Models;
using BancoDeSangue.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BancoDeSangue.Services
{
    public class AutenticacaoService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        private readonly IUsuarioRepository usuarioRepository = usuarioRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly PasswordHasher<Usuario> hasher = new();

        public async Task<bool> RegistrarAsync(string nome, string email, string senha)
        {
            if (await usuarioRepository.RecuperarUsuarioPorEmailAsync(email) is not null)
                return false;

            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                SenhaHash = hasher.HashPassword(null, senha) 
            };

            await unitOfWork.UsuarioRepository.CriarAsync(usuario);
            await unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> LoginAsync(string email, string senha)
        {
            var usuario = await usuarioRepository.RecuperarUsuarioPorEmailAsync(email);

            if (usuario is null)
                return false;

            var resultado = hasher.VerifyHashedPassword(usuario, usuario.SenhaHash, senha);

            return true;
        }
    }

}

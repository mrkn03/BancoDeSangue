using BancoDeSangue.Models;

namespace BancoDeSangue.Repository.Interfaces
{
    public interface IDoadorRepository
    {
        Doador CadastrarDoador(Doador doador);
        Doador RecuperarDoador(string cpf);
        Doador AtualizarDoador(Doador doador);
        Doador DeletarDoador(string cpf);
        IEnumerable<Doador> ListarDoadores();
    }
}

using BancoDeSangue.Data;
using BancoDeSangue.DTOs;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeSangue.Repository.Interfaces
{
    public interface IDoacaoRepository : IRepository<Doacao>
    {
        Doacao CriarDoacao(string cpf, int quantidadeML);
        IEnumerable<DoacaoDTO> ListarDoacoes();
    }
}

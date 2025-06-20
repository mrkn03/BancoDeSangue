using BancoDeSangue.Data;
using BancoDeSangue.DTOs;
using BancoDeSangue.Models;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeSangue.Repository.Interfaces
{
    public interface IDoacaoRepository : IRepository<Doacao>
    {
        Task<Doacao> CriarDoacaoAsync(string cpf, int quantidadeML);
        Task<IEnumerable<DoacaoDTO>> ListarDoacoesAsync();
    }
}

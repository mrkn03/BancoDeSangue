﻿using BancoDeSangue.Data;
using BancoDeSangue.Models;
using BancoDeSangue.Repositories.Interfaces;
using BancoDeSangue.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeSangue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public EstoqueController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<EstoqueDeSangue>> ObterEstoque()
        {

            var estoque = await unitOfWork.EstoqueDeSangueRepository.RecuperarPorIdAsync(e => e.Id == 1);

            if (estoque == null)
            {
                return NotFound();
            }

            return Ok(estoque);
        }
    }
}

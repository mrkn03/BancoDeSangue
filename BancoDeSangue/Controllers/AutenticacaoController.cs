using BancoDeSangue.DTOs;
using BancoDeSangue.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeSangue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService autenticacaoService;

        public AutenticacaoController(AutenticacaoService autenticacaoService)
        {
            this.autenticacaoService = autenticacaoService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar([FromBody] RegistrarDTO registrarDTO)
        {
            var registro = await autenticacaoService.RegistrarAsync(registrarDTO.Nome, registrarDTO.Email, registrarDTO.Senha);
            
            if (registro)
            {
                return Ok("Usuário registrado com sucesso.");
            }
            else
            {
                return BadRequest("Erro ao registrar usuário. Verifique os dados e tente novamente.");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var login = await autenticacaoService.LoginAsync(loginDTO.Email, loginDTO.Senha);

            if (login)
            {
                return Ok("Login realizado com sucesso.");
            }
            else
            {
                return Unauthorized("Email ou senha inválidos.");
            }
        }
    }
}

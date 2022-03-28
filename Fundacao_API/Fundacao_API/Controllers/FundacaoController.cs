using Fundacao_API.Repositories;
using Fundacao_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Fundacao_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FundacaoController : ControllerBase
    {

        private readonly FundacaoService _fundacaoService;

        public FundacaoController(FundacaoService fundacaoService)
        {
            _fundacaoService = fundacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFundacoes()
        {

            var fundacoes = await _fundacaoService.GetAllFundacoes();
            if (fundacoes != null)
            {
                return Ok(fundacoes);
            }
            else
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        
        [HttpGet("{IDFundacao}")]
        public async Task<IActionResult> GetFundacao(int IDFundacao)
        {

            var fundacao = await _fundacaoService.GetFundacao(IDFundacao);
            if (fundacao != null)
            {
                return Ok(fundacao);
            }
            else
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Essa Fundação Não Existe");
            }
        }


        [HttpPost()]
        public async Task<IActionResult> AddFundacao(Fundacao Fundacao)
        {
            var fundacao =  await _fundacaoService.AdicionarFundacao(Fundacao);
            if (fundacao)
            {
                return Ok("Fundação adicionada com sucesso");
            }
            else
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Não foi possível cadastrar fundação");
            }
        }


        [HttpPut]
        public async Task<IActionResult> AlterarFundacao(Fundacao Fundacao)
        {
            var fundacao = await _fundacaoService.AlterarFundacao(Fundacao);
            if (fundacao)
            {
                return Ok("Fundação alterada com sucesso");
            }
            else
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Não foi possível alterar fundação");
            }
        }


        [HttpDelete("{IDFundacao}")]
        public async Task<IActionResult> deleteFundacao(int IDFundacao)
        {
            var fundacao = await _fundacaoService.DeletarFundacao(IDFundacao);
            if (fundacao)
            {
                return Ok("Fundação removida com sucesso");
            }
            else
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Não foi possível cadastrar fundação");
            }
        }


    }
}

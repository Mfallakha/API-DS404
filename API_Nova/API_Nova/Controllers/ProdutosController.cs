using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoEscola_API.Data;
using ProjetoEscola_API.Models;

namespace ProjetoEscola_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
       private  academiaContext _context;
       public ProdutoController(academiaContext context)
        {
            // construtor
            _context = context;
        }

        [HttpGet]
public ActionResult<List<Produto>> GetAll()
{
return _context.Produto.ToList();
}

        [HttpPost]
        public async Task<ActionResult> post(Produto model)
        {
            try
            {
                _context.Produto.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/Produto/{model.codProduto}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
           
            return BadRequest();
        }

        [HttpGet("{ProdutoId}")]
        public ActionResult<List<Produto>> Get(int ProdutoId)
        {
            try
            {
                var result = _context.Produto.Find(ProdutoId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
        [HttpDelete("{ProdutoId}")]
        public async Task<ActionResult> delete(int ProdutoId)
        {
            try
            {
                //verifica se existe Produto a ser excluído
                var Produto = await _context.Produto.FindAsync(ProdutoId);
                if (Produto == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(Produto);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
        [HttpPut("{ProdutoId}")]
        public async Task<IActionResult> put(int ProdutoId, Produto dadosProdutoAlt)
        {
            
            try
            {
                var result = await _context.Produto.FindAsync(ProdutoId);
                if (ProdutoId != result.id)
                {
                    return BadRequest();
                }
                result.codProduto = dadosProdutoAlt.codProduto;
                result.nomeProduto = dadosProdutoAlt.nomeProduto;
                result.valorProduto = dadosProdutoAlt.valorProduto;
                result.imagem = dadosProdutoAlt.imagem;
                result.comprado = dadosProdutoAlt.comprado;
                 result.compradoPor = dadosProdutoAlt.compradoPor;
                await _context.SaveChangesAsync();
                return Created($"/api/Produto/{dadosProdutoAlt.codProduto}", dadosProdutoAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }


        }
    }
}
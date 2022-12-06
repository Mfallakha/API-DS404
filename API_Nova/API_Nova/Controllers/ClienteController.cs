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
    public class clienteController : ControllerBase
    {
        private academiaContext _context;
        public clienteController(academiaContext context)
        {
            // construtor
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<cliente>> GetAll()
        {
            return _context.Cliente.ToList();
        }

        [HttpGet("{clienteId}")]
        public ActionResult<List<cliente>> Get(int clienteId)
        {
            try
            {
                var result = _context.Cliente.Find(clienteId);
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
        [HttpPost]
        public async Task<ActionResult> post(cliente model)
        {
            try
            {
                _context.Cliente.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/cliente/{model.username}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }
        [HttpDelete("{clienteId}")]
        public async Task<ActionResult> delete(int clienteId)
        {
            try
            {
                //verifica se existe cliente a ser excluído
                var cliente = await _context.Cliente.FindAsync(clienteId);
                if (cliente == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(cliente);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falhano acesso ao banco de dados.");
            }

        }
        [HttpPut("{clienteId}")]
        public async Task<IActionResult> put(int clienteId, cliente dadosclienteAlt)
        {
            try
            {
                //verifica se existe cliente a ser alterado
                var result = await _context.Cliente.FindAsync(clienteId);
                if (clienteId != result.id)
                {
                    return BadRequest();
                }
                result.username = dadosclienteAlt.username;
                result.senha = dadosclienteAlt.senha;
                result.role = dadosclienteAlt.role;
                await _context.SaveChangesAsync();
                return Created($"/api/cliente/{dadosclienteAlt.username}", dadosclienteAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
    }

}
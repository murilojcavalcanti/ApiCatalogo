using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProdutosController : ControllerBase
    {
        private AppDbContext Context;

        public ProdutosController(AppDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get(int take = 10, int categoriaid = 0)
        {
            List<Produto> produtos = null;
            try
            {
                if ( categoriaid > 0)
                {
                    produtos = Context.Produtos.AsNoTracking().Take(take).Where(p => p.categoriaId == categoriaid).ToList();

                }
                else{
                    produtos = Context.Produtos.AsNoTracking().Take(take).ToList();
                }
                if (produtos is null) return NotFound("Produtos não encontrados");
                return produtos;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                var produto = Context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound($"Produto com id-> {id} não encontrado!");
                }
                return produto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            try
            {
                if (produto is null) return BadRequest("Dados Invalidos");
                Context.Produtos.Add(produto);
                Context.SaveChanges();
                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId) return BadRequest("Dados Invalidos");

                Context.Entry(produto).State = EntityState.Modified;
                Context.SaveChanges();
                return Ok();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var produto = Context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                if (produto is null) return NotFound($"Produto com id -> {id} não localizado...");
                Context.Produtos.Remove(produto);
                Context.SaveChanges();
                return Ok(produto);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}

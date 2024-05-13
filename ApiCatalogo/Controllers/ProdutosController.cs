using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProdutosController:ControllerBase
    {
        private AppDbContext Context;

        public ProdutosController(AppDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>>Get(int take = 0, int categoriaid = 0)
        {
            List<Produto> produtos = null;

            if (take != 0 && categoriaid !=0)
            {
                 produtos = Context.Produtos.AsNoTracking().Take(take).Where(p=>p.categoriaId==categoriaid).ToList();

            }else if(take != 0 && categoriaid == 0)
            {
                produtos = Context.Produtos.AsNoTracking().Take(take).ToList();

            }
            else if (take == 0 && categoriaid != 0)
            {
                produtos = Context.Produtos.AsNoTracking().Where(p => p.categoriaId == categoriaid).ToList();
            }
            else
            {
                produtos = Context.Produtos.AsNoTracking().ToList();
            }
            if (produtos is null) return NotFound("Produtos não encontrados");
            return produtos;
        }

        [HttpGet("{id:int}",Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = Context.Produtos.AsNoTracking().FirstOrDefault(p=>p.ProdutoId==id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado!");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null) return BadRequest();
            Context.Produtos.Add(produto);  
            Context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto",new { id=produto.ProdutoId}, produto);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id!=produto.ProdutoId) return BadRequest();

            Context.Entry(produto).State = EntityState.Modified;
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = Context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null) return NotFound("Produto não localizado...");
            Context.Produtos.Remove(produto);
            Context.SaveChanges();
            return Ok(produto);
        }


    }
}

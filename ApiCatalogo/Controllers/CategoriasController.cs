using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoriasController:ControllerBase
    {
        private AppDbContext Context;

        public CategoriasController(AppDbContext context)
        {
            Context = context;
        }
        
        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            var categoriasProdutos = Context.Categorias.Include(p=>p.Produtos).ToList();
            if (categoriasProdutos is null) return NotFound("Nenhuma categoria foi encontrada...");
            return categoriasProdutos;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = Context.Categorias.AsNoTracking().ToList();
            if(categorias is null) return NotFound("Nenhuma categoria foi encontrada...");
            return categorias;
        }
        [HttpGet("{id:int}",Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = Context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null) return NotFound("Categoria não encontrada...");

            return categoria;
        }
        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria == null) return BadRequest();
            
            Context.Categorias.Add(categoria);
            Context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new {id=categoria.CategoriaId}, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id,Categoria categoria)
        {
            if(id!=categoria.CategoriaId) return BadRequest();
            Context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = Context.Categorias.FirstOrDefault(c=>c.CategoriaId==id);

            if(categoria is null) return BadRequest();

            Context.Categorias.Remove(categoria);
            Context.SaveChanges();
            return Ok(categoria);

        }
    }
}

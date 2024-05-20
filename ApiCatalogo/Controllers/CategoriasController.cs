using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoriasController : ControllerBase
    {
        private AppDbContext Context;

        public CategoriasController(AppDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutosAsync(int take = 10)
        {
            try
            {

                var categoriasProdutos = Context.Categorias.Include(p => p.Produtos).Take(10).ToListAsync();
                if (categoriasProdutos is null) return NotFound("Nenhuma categoria foi encontrada...");
                return await categoriasProdutos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        
        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetAsync(int id)
        {
            try
            {
                var categoria = await Context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id);
                if (categoria is null) return NotFound("Categoria não encontrada...");

                return categoria;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria == null) return BadRequest("Dados inválidos");

                Context.Categorias.Add(categoria);
                Context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Categoria categoria)
        {

            try
            {
                if (id != categoria.CategoriaId) return BadRequest("Dados inválidos");
                Context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Context.SaveChanges();
                return Ok(categoria);
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
                var categoria = Context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

                if (categoria is null) return BadRequest("Dados Inválidos");

                Context.Categorias.Remove(categoria);
                Context.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}

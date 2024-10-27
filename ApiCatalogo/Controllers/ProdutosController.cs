using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.RepositoryProduto;
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
        private readonly IProdutoRepository ProdutoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            ProdutoRepository = produtoRepository;
        }

        [HttpGet("Produto/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutoCategoria(int id, IProdutoRepository produtoRepository)
        {
            List<Produto> produtos = ProdutoRepository.GetProdutoByCategoria(id).ToList();
            if (produtos is null)
                return NotFound();

            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetAll(int take = 10)
        {
            List<Produto> produtos;
            try
            {
                produtos = ProdutoRepository.GetAll().Take(take).ToList();
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
                Produto produto = ProdutoRepository.Get(p => p.ProdutoId == id);
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
                Produto produtoNew = ProdutoRepository.create(produto);
                return new CreatedAtRouteResult("ObterProduto", new { id = produtoNew.ProdutoId }, produtoNew);
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
                if (id != produto.ProdutoId) 
                    return BadRequest("Dados Invalidos");

                Produto produtoUpdated = ProdutoRepository.Update(produto);
                return Ok(produtoUpdated);

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
                var produto = ProdutoRepository.Get(p=>p.ProdutoId==id);
                if (produto is null) return NotFound($"Produto com id -> {id} não localizado...");
                Produto produtoDeleted = ProdutoRepository.Delete(produto);
                return Ok(produtoDeleted);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}

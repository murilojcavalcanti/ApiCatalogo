using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.RepositoryProduto;
using ApiCatalogo.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProdutosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Produto/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutoCategoria(int id, IProdutoRepository produtoRepository)
        {
            List<Produto> produtos = _unitOfWork.ProdutoRepository.GetProdutoByCategoria(id).ToList();
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
                produtos = _unitOfWork.ProdutoRepository.GetAll().Take(take).ToList();
                if (produtos is null) return NotFound("Produtos não encontrados");

                return produtos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                Produto produto =  _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
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
                Produto produtoNew = _unitOfWork.ProdutoRepository.create(produto);
                _unitOfWork.Commit();
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

                Produto produtoUpdated = _unitOfWork.ProdutoRepository.Update(produto);
                _unitOfWork.Commit();
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
                var produto =_unitOfWork.ProdutoRepository.Get(p=>p.ProdutoId==id);
                if (produto is null) return NotFound($"Produto com id -> {id} não localizado...");
                Produto produtoDeleted = _unitOfWork.ProdutoRepository.Delete(produto);
                _unitOfWork.Commit();
                return Ok(produtoDeleted);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}

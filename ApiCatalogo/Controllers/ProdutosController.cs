using ApiCatalogo.Context;
using ApiCatalogo.DTO.ProdutoDto;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.RepositoryProduto;
using ApiCatalogo.Repositories.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("Produto/{id}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutoCategoria(int id, IProdutoRepository produtoRepository)
        {
            try
            {

                List<Produto> produtos = _unitOfWork.ProdutoRepository.GetProdutoByCategoria(id).ToList();
                if (produtos is null)
                    return NotFound();

                IEnumerable<ProdutoDTO> produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

                return Ok(produtosDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");

            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> GetAll(int take = 10)
        {
            List<Produto> produtos;
            try
            {
                produtos = _unitOfWork.ProdutoRepository.GetAll().Take(take).ToList();
                if (produtos is null) return NotFound("Produtos não encontrados");

                List<ProdutoDTO> produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);


                return produtosDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            try
            {
                Produto produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound($"Produto com id-> {id} não encontrado!");
                }
                ProdutoDTO produtoDTO = _mapper.Map<ProdutoDTO>(produto);
                return produtoDTO;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDTO)
        {
            try
            {
                if (produtoDTO is null) return BadRequest("Dados Invalidos");
                
                Produto produto = _mapper.Map<Produto>(produtoDTO);
                Produto produtoNew = _unitOfWork.ProdutoRepository.create(produto);
                _unitOfWork.Commit();

                ProdutoDTO produtoDtoNew = _mapper.Map<ProdutoDTO>(produto);
                return new CreatedAtRouteResult("ObterProduto", new { id = produtoDtoNew.ProdutoId }, produtoDtoNew);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
        [HttpPut("{id:int:min(1)}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO)
        {
            try
            {
                if (id != produtoDTO.ProdutoId)
                    return BadRequest("Dados Invalidos");

                Produto produto = _mapper.Map<Produto>(produtoDTO);
                Produto produtoUpdated = _unitOfWork.ProdutoRepository.Update(produto);
                _unitOfWork.Commit();

                ProdutoDTO produtoDtoUpdated = _mapper.Map<ProdutoDTO>(produtoUpdated);
                return Ok(produtoDtoUpdated);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }


        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<ResponseUpdateProdutoDTO> Patch(int id, JsonPatchDocument<RequestUpdateProdutoDTO> patchProdutoDto)
        {
            if (patchProdutoDto is null || id<=0) return BadRequest();

            Produto produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null) return NotFound();

            RequestUpdateProdutoDTO requestUpdateProduto = _mapper.Map<RequestUpdateProdutoDTO>(produto);
            if (requestUpdateProduto is null) return NotFound("Ocorreu um erro ao mapear o produto!");

            //Aplicando as alterações parciais do patchprodutodto no produto.
            patchProdutoDto.ApplyTo(requestUpdateProduto,ModelState);

            //valida se as alterações foram aplicadas.
            if (!ModelState.IsValid || TryValidateModel(requestUpdateProduto))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(requestUpdateProduto, produto);
            _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();


            return Ok(_mapper.Map<ResponseUpdateProdutoDTO>(produto));


        }


        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            try
            {
                Produto produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
                if (produto is null) return NotFound($"Produto com id -> {id} não localizado...");
                Produto produtoDeleted = _unitOfWork.ProdutoRepository.Delete(produto);
                _unitOfWork.Commit();
                ProdutoDTO produtoDtoDeleted = _mapper.Map<ProdutoDTO>(produto);

                return Ok(produtoDtoDeleted);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}

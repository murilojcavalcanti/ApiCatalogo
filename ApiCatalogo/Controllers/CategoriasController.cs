﻿using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly IRepository<Categoria> Repository;
        //Usando a interface Iconfiguration atraves da injeção de dependencias
        private readonly IConfiguration Configuration;
        //Definindo uma instancia da interface ILogger
        private readonly ILogger Logger;
        //
        public CategoriasController(AppDbContext context, IConfiguration configuration,
            //Injetando a instancia no construtor
            ILogger<CategoriasController> logger, IRepository<Categoria> repository)
        {
            Configuration = configuration;
            Logger = logger;
            Repository = repository;
        }


        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutosAsync(int take =10)
        {
            try
            {
                Logger.LogInformation("================== GET api/Categorias/Produtos ===================================");

                var categoriasProdutos = Repository.GetAll().Take(take).ToList();
                if (categoriasProdutos is null) return NotFound("Nenhuma categoria foi encontrada...");
                return categoriasProdutos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        
        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetAsync(int id)
        {
            try
            {
                Logger.LogInformation($"================== GET api/Categorias/id={id} ===================================");
                var categoria = Repository.Get(c=>c.CategoriaId==id);
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

               Categoria categoriaCriada =  Repository.create(categoria);
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
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
                Categoria CategoriaAtualizada = Repository.Update(categoria);
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
                var categoria = Repository.Get(c=>c.CategoriaId==id);

                if (categoria is null) return BadRequest("Dados Inválidos");

                Categoria categoriaExcluida = Repository.Delete(categoria);
                return Ok(categoriaExcluida);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}

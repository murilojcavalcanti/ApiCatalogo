using ApiCatalogo.DTO.CategoriaDto;
using ApiCatalogo.DTO.Mapping.CategoriaMapping;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;


namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoriasController : ControllerBase
    {
        /*Ao implementar o unit of work deixamos de precisar injetar o repository, pois ele ja é implementado em Unit Of Work*/
        
        //Definindo uma instancia da interface ILogger
        private readonly ILogger Logger;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriasController(//Injetando a instancia no construtor
                                    ILogger<CategoriasController> logger, IRepository<Categoria> repository, IUnitOfWork unitOfWork)
        {
            Logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<CategoriaDTO>> GetAll(int take =10)
        {
            try
            {
                Logger.LogInformation("================== GET api/Categorias/Produtos ===================================");

                List<Categoria> categoriasProdutos = _unitOfWork.CategoriaRepository.GetAll().Take(take).ToList();
                if (categoriasProdutos is null) return NotFound("Nenhuma categoria foi encontrada...");

                //Usando o metodo de extensão para converter a classe concreta em um DTOs
                List<CategoriaDTO> categoriasDTO = CategoriaDTOMappingExtensions.ToCategoriaDTOToList(categoriasProdutos).ToList();
                return categoriasDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        
        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> GetAsync(int id)
        {
            try
            {
                Logger.LogInformation($"================== GET api/Categorias/id={id} ===================================");
                Categoria categoria = _unitOfWork.CategoriaRepository.Get(c=>c.CategoriaId==id);
                
                
                if (categoria is null) return NotFound("Categoria não encontrada...");

                CategoriaDTO categoriaDTO = CategoriaDTOMappingExtensions.ToCategoriaDTO(categoria);

                return categoriaDTO;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            try
            {
                if (categoriaDto == null) 
                {
                    return BadRequest("Dados inválidos"); 
                }

                Categoria categoria = CategoriaDTOMappingExtensions.ToCategoria(categoriaDto);

                Categoria categoriaCriada =  _unitOfWork.CategoriaRepository.create(categoria);
                _unitOfWork.Commit();

                CategoriaDTO categoriaDTONovo = CategoriaDTOMappingExtensions.ToCategoriaDTO(categoriaCriada);
                
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaDTONovo.CategoriaId }, categoriaDTONovo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO)
        {

            try
            {
                if (id != categoriaDTO.CategoriaId) return BadRequest("Dados inválidos");

                Categoria categoria = CategoriaDTOMappingExtensions.ToCategoria(categoriaDTO);
                
                Categoria CategoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
                _unitOfWork.Commit();

                CategoriaDTO CategoriaDTOAtualizada = CategoriaDTOMappingExtensions.ToCategoriaDTO(CategoriaAtualizada);

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            try
            {
                var categoria = _unitOfWork.CategoriaRepository.Get(c=>c.CategoriaId==id);

                if (categoria is null) return BadRequest("Dados Inválidos");

                Categoria categoriaExcluida = _unitOfWork.CategoriaRepository.Delete(categoria);
                _unitOfWork.Commit();

                CategoriaDTO categoriaDTOExcluida = CategoriaDTOMappingExtensions.ToCategoriaDTO(categoriaExcluida);

                return Ok(categoriaExcluida);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao tratar a sua solicitação");
            }
        }
    }
}

using ApiCatalogo.DTO.CategoriaDto;
using ApiCatalogo.Models;

namespace ApiCatalogo.DTO.Mapping.CategoriaMapping
{
    public static class CategoriaDTOMappingExtensions
    {
        //Mapeando uma categoria para uma categoriaDTO
        public static CategoriaDTO ToCategoriaDTO( this Categoria categoria)
        {
            if (categoria is null) return null;

            return new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };

        }


        //Mapeando uma categoriaDTO para uma categoria
        public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO is null) return null;

            return new Categoria
            {
                CategoriaId = categoriaDTO.CategoriaId,
                Nome = categoriaDTO.Nome,
                ImagemUrl= categoriaDTO.ImagemUrl
            };
        }

        //Mapeando uma Lista de categoria para uma Lista de categoriaDTO
        public static IEnumerable<CategoriaDTO>? ToCategoriaDTOToList(IEnumerable<Categoria> categorias)
        {
            if (categorias is null || !categorias.Any()) return new List<CategoriaDTO>();

            return categorias.Select(categoria => new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            }).ToList();
        }
    }
}

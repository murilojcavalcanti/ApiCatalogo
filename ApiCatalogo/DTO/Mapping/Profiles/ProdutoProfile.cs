using ApiCatalogo.DTO.CategoriaDto;
using ApiCatalogo.DTO.ProdutoDto;
using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTO.Mapping.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
        }
    }
}

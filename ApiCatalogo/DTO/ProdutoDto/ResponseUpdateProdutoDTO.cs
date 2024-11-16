using ApiCatalogo.Models;
using ApiCatalogo.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiCatalogo.DTO.ProdutoDto;
public class ResponseUpdateProdutoDTO
{
    public int ProdutoId { get; set; }
    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int categoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}

namespace ApiCatalogo.Models
{
    public class Produto
    {

        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Descrição { get; set; }
        public decimal Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int categoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}

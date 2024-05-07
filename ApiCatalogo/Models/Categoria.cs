using System.Collections.ObjectModel;

namespace ApiCatalogo.Models
{
    public class Categoria
    {
        //É uma boa prática inicializar uma propriedade collection no construtor
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }
        public int CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string? imagem{ get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}

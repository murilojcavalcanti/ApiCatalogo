using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Models
{
    public class Categoria
    {
        //É uma boa prática inicializar uma propriedade collection no construtor
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? imagem{ get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}

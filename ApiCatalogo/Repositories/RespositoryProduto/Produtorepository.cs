using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.RepositoryProduto;

namespace ApiCatalogo.Repositories.RespositoryProduto
{
    public class Produtorepository : Repository<Produto>, IProdutoRepository
    {
        public Produtorepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Produto> GetProdutoByCategoria(int id)
        {
            return GetAll().Where(c => c.categoriaId == id);
        }
    }
}

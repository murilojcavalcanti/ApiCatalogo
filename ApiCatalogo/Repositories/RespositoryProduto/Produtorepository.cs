using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories.RepositoryProduto;

namespace ApiCatalogo.Repositories.RespositoryProduto
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Produto> GetProdutoByCategoria(int id)
        {
            return GetAll().Where(c => c.categoriaId == id);
        }
    }
}

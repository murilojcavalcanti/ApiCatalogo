using ApiCatalogo.Models;
using System.Collections.Generic;

namespace ApiCatalogo.Repositories.RepositoryProduto
{
    public interface IProdutoRepository:IRepository<Produto>
    {
       
        IEnumerable<Produto> GetProdutoByCategoria(int id);
    }
}



using ApiCatalogo.Context;
using ApiCatalogo.Models;
using System.Linq.Expressions;

namespace ApiCatalogo.Repositories.RepositoryCategoria
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        //Caso a classe precise usar o context, ela irá usar a instancia de context da classe base, já injetada.
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }
    }
}

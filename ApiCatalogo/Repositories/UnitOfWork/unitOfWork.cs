using ApiCatalogo.Context;
using ApiCatalogo.Repositories.RepositoryCategoria;
using ApiCatalogo.Repositories.RepositoryProduto;
using ApiCatalogo.Repositories.RespositoryProduto;

namespace ApiCatalogo.Repositories.UnitOfWork
{
    public class unitOfWork : IUnitOfWork
    {
        private IProdutoRepository? _produtoRepository;

        private ICategoriaRepository? _categoriaRepository;

        public AppDbContext _context;

        public unitOfWork(AppDbContext context)
        {
            _context = context;
        }

        //Essa implementação é chamada de lazyLoading significa adiar a obtenção dos objetos até que eles sejam realmente necessários.
        //Implementamos essa propriedade para obter uma instancia do repositorio de produtos, e toda vez que a propriedade for usada, vamos
        //verifica se ja tem uma instancia de produtoRepository caso não tenha, é criada uma nova.
        //Não injetamos as instancias no construtor para que não fosse criada uma nova instancia todas as vezes que o construtor fosse chamado. 
        public IProdutoRepository ProdutoRepository
        {
            get
            {//outra abordagem
                /*if (_produtoRepository is null)
                    _produtoRepository = new ProdutoRepository(_context);

                return _produtoRepository;*/
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void dispose()
        {
            //libera recursos do sistema
            _context.Dispose();
        }
    }
}

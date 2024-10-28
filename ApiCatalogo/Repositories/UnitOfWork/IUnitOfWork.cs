using ApiCatalogo.Repositories.RepositoryCategoria;
using ApiCatalogo.Repositories.RepositoryProduto;

namespace ApiCatalogo.Repositories.UnitOfWork
{
    //Essa interface irá abstrair e agrupar as operações
    //relacionadas aos repositorios e ela também tem que fazer a percistencia 
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }

        //Ao usar propriedade encapsulamos o acesso aos repositorios e eu permito maior controle da forma
        //como eles serão expostos e utilizados, além disso é mais facil quando formos realizar testes de unidades

        void Commit();


    }
}

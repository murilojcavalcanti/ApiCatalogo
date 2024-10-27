using System.Linq.Expressions;

namespace ApiCatalogo.Repositories
{
    /*
     * Repositorio generico 
     * Aqui acontece a definição da interface com todas as operações comuns as entidades
     */
    public interface IRepository<T>
    {
        //Realizar prestando atenção no principio ISP(Princípio da Segregação da Interface)
        IEnumerable<T> GetAll();

        /*Expression é usada para representar expressões lambda
        Func é um delegate que é uma função que pode ser passada como argumento,
        que representa uma função lambda que vai receber um objeto do tipo T e retorna um boleano, e retorna com
        base no predicado que será o criterio usado para filtrar
        */
        T? Get(Expression<Func<T,bool>>predicate);

        T create(T entity);
        
        T Update(T entity);

        T Delete(T entity);


    }
}

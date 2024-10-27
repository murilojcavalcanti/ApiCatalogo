using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories
{

    /*
     * Classe generica que vai implementar a interface Repository
     * nesta classe também foi adicionada uma clausula para que o tipo T seja uma classe
     */
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext Context;

        public Repository(AppDbContext context)
        {
            Context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
            /*
             O metodo set é usado para acessar uma coleção ou uma tabela
             */
        }
        public T? Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
            /*
             * Vai buscar o primeiro resultado que obedecer ao predicate ou retornará nulo
             */
        }


        public T create(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return entity;
        }
       

        public T Update(T entity)
        {
            Context.Set<T>().Update(entity);//Indicado para atualizações completas
            /*Context.Entry(entity).State=EntityState.Modified;
             Define explicitamente o estado da entidade como modificado, então o entity framework vai 
             identificar que a entidade foi alterada e gera um sql de atualização para a entidade
             */
            Context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
            return entity;
        }

    }
}

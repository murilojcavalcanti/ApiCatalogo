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
            //O .AsNoTracking() é usado para realizar uma otimização no codigo, ele faz com que o entity framework
            //core pare de gerenciar os estados das entidades na memoria. usado apenas para consulta
            return Context.Set<T>().AsNoTracking().ToList();
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
            //Context.SaveChanges();
            //Agora iremos usar o padrão UnitOfWork para realizar o salvamento
            return entity;
        }
       

        public T Update(T entity)
        {
            //Indicado para atualizações completas
            /*Context.Entry(entity).State=EntityState.Modified;
             Define explicitamente o estado da entidade como modificado, então o entity framework vai 
             identificar que a entidade foi alterada e gera um sql de atualização para a entidade
             */
            Context.Set<T>().Update(entity);
            //Context.SaveChanges();
            //Agora iremos usar o padrão UnitOfWork para realizar o salvamento
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

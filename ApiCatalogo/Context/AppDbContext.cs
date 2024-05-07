using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        DbSet<Categoria> Categorias { get; set; }
        DbSet<Produto> Produtos { get; set; }
    }
}

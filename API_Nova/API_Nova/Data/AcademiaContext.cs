using Microsoft.EntityFrameworkCore;
using ProjetoEscola_API.Models;
using System.Diagnostics.CodeAnalysis;
namespace ProjetoEscola_API.Data
{
    public class academiaContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public academiaContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("StringConexaoSQLServer"));
        }

        public DbSet<Aluno>? Aluno { get; set; }
        public DbSet<Curso>? Curso { get; set; }
        public DbSet<cliente>? Cliente { get; set; }

         public DbSet<Produto>? Produto { get; set; }
         
  
    }
}
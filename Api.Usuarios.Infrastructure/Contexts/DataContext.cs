using Api.Usuarios.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Api.Usuarios.Infrastructure.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=sqlserver,1433;Initial Catalog=master;User ID=sa;Password=Coti@2026;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}

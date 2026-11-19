using Api.Usuarios.Domain.Entities;
using Api.Usuarios.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Usuarios.Infrastructure.Repositories
{
    public class PerfilRepository
    {
        // Método para inserir um perfil no banco de dados
        public async Task InserirAsync(Perfil perfil)
        {
            using (var dataContext = new DataContext())
            {
                // Inserindo o perfil na tabela
                await dataContext.AddAsync(perfil);

                // Salvando e Executando
                await dataContext.SaveChangesAsync();
            }
        }

        // Método para obter 1 Perfil no banco de dados através do Nome
        public async Task<Perfil?> ObterAsync(string nome)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Set<Perfil>()
                    .FirstOrDefaultAsync(p => p.Nome.Equals(nome));
            }
        }
    }
}

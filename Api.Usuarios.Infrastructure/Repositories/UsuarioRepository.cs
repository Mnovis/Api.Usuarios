using Api.Usuarios.Domain.Entities;
using Api.Usuarios.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Api.Usuarios.Infrastructure.Repositories
{
    public class UsuarioRepository
    {
        public async Task InserirAsync(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                await dataContext.AddAsync(usuario);
                await dataContext.SaveChangesAsync();
            }
        }

        // Método para verificar no Banco de Dados se o Email de um Usuário já está cadastrado.
        public async Task<bool> VerifyEmailAsync(string email)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Set<Usuario>()
                    .FirstOrDefaultAsync(u => u.Email.Equals(email)) != null;
            }
        }

        // Método para consultar no Banco de Dados um Usuário
        // Através do Email e Senha
        public async Task<Usuario?> FindAsync(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Set<Usuario>()
                    .Include( u => u.Perfil)
                    .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Senha.Equals(senha));


            }
        }
    }
}

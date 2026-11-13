using Api.Usuarios.Domain.Entities;
using Api.Usuarios.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Usuarios.Infrastructure.Repositories
{
    public class UsuarioRepository
    {
        //Método para inserir um usuário no banco de dados
        public async Task InserirAsync(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                await dataContext.AddAsync(usuario);
                await dataContext.SaveChangesAsync();
            }
        }

        //Método para verificar no banco de dados se o email
        //de um usuário já está cadastrado na tabela
        public async Task<bool> VerifyEmailAsync(string email)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Set<Usuario>()
                    .FirstOrDefaultAsync(u => u.Email.Equals(email)) != null;
            }
        }

        //Método para consultar no banco de dados 1 usuário
        //Através do email e da senha informado
        public async Task<Usuario?> FindAsync(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Set<Usuario>()
                    .FirstOrDefaultAsync(u => u.Email.Equals(email)
                                           && u.Senha.Equals(senha));
            }
        }
    }
}

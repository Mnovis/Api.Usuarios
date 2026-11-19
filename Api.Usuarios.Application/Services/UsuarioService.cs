using Api.Usuarios.Application.DTOs;
using Api.Usuarios.Application.Helpers;
using Api.Usuarios.Domain.Entities;
using Api.Usuarios.Domain.Validators;
using Api.Usuarios.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Security.Cryptography;
using System.Text;

namespace Api.Usuarios.Application.Services
{
    public class UsuarioService
    {
        //Método para criar um usuário
        public async Task<CriarUsuarioResponse> CriarUsuario(CriarUsuarioRequest request)
        {
            #region Capturar os dados enviados na requisição (REQUEST)

            var usuario = new Usuario
            {
                Nome = request.nome,
                Email = request.email,
                Senha = request.senha
            };

            #endregion

            #region Verificar se os dados do usuário passam nas validações (FLUENT VALIDATION)

            var usuarioValidator = new UsuarioValidator();
            var result = usuarioValidator.Validate(usuario);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            #endregion

            #region Verificar se o email informado já está cadastrado no banco de dados (EF)

            var usuarioRepository = new UsuarioRepository();
            var emailEncontrado = await usuarioRepository.VerifyEmailAsync(usuario.Email);

            if (emailEncontrado)
            {
                throw new ApplicationException("O email informado já está cadastrado, tente outro.");
            }

            #endregion

            #region Vincular o usuário a um perfil chamado "Operador"

            var perfilRepository = new PerfilRepository();
            var perfil = await perfilRepository.ObterAsync("Operador");

            if (perfil == null)
            {
                perfil = new Perfil { Nome = "Operador" };
                await perfilRepository.InserirAsync(perfil);
            }

            usuario.PerfilId = perfil.Id; //Chave estrangeira

            #endregion

            #region Criptografar a senha do usuário

            usuario.Senha = CriptografarSenha(usuario.Senha);

            #endregion

            #region Salvar o usuário no banco de dados

            await usuarioRepository.InserirAsync(usuario);

            #endregion

            #region Retornar a resposta (RESPONSE)

            return new CriarUsuarioResponse(
                    "Usuário cadastrado com sucesso",
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    perfil.Nome,
                    DateTime.Now
                );

            #endregion
        }

        //Método para autenticar o usuário
        public async Task<AutenticarUsuarioResponse> AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            #region Obter o usuário no banco de dados através do Email e Senha

            var senhaCriptografada = CriptografarSenha(request.senha);

            var usuarioRepository = new UsuarioRepository();

            var usuario = await usuarioRepository.FindAsync(request.email, senhaCriptografada);

            #endregion

            #region Se o usuário não for encontrado o sistema deve retornar acesso negado

            if (usuario == null)
            {
                throw new ApplicationException("Acesso negado, usuário inválido.");
            }

            #endregion

            #region Gerar o token do usuário

            var jwtTokenHelper = new JwtTokenHelper();
            var acessToken = jwtTokenHelper.GenerateToken(usuario.Email, usuario.Perfil!.Nome);

            #endregion

            #region Retornar os dados do usuário autenticado

            return new AutenticarUsuarioResponse(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Perfil!.Nome,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                acessToken
             );

            #endregion
        }

        private string CriptografarSenha(string senha)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(senha);
            byte[] hash = SHA256.HashData(bytes);

            return Convert.ToHexString(hash);
        }
    }
}

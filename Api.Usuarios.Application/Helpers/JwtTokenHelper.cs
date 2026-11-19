using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Usuarios.Application.Helpers
{
    public class JwtTokenHelper
    {
        #region Atributos Privados

        // Atributo para armazenar a chave de criptografia do Token JWT
        private readonly string _key = "83e22814-ed47-48ee-a0b7-df29aea9c76c";

        // Atributo para armazenar o tempo de expiração do Token JWT
        private readonly int _expirationInHours = 1;

        #endregion

        #region Métodos

        // Método para gerar o Token
        public string GenerateToken(string email, string perfil)
        {
            #region Calcular a data de geração e de expiração do Token

            var dataAtual = DateTime.UtcNow;
            var dataExpiracao = dataAtual.AddHours(_expirationInHours);

            #endregion

            #region Gerar a chave usada para criptografar e assinar o Token

            var signature = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            #endregion

            #region Gerar os dados que serão gravados no conteúdo do Token

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = "Api.Usuarios", //Nome da aplicação que gerou o token
                IssuedAt = dataAtual, //Data de geração do token
                NotBefore = dataAtual, //Data que o token passa a ser válido
                Expires = dataExpiracao, //Data de expiração do token
                Claims = new Dictionary<string, object>
                {
                    [JwtRegisteredClaimNames.Email] = email,
                    ["role"] = perfil
                },
                SigningCredentials = new SigningCredentials(signature, SecurityAlgorithms.HmacSha256)
            };

            #endregion

            #region Gerar e retornar o Token

            var token = new JsonWebTokenHandler().CreateToken(descriptor);
            return token;

            #endregion
        }

        #endregion

    }
}

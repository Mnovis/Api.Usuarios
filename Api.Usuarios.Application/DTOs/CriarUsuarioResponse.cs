using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Usuarios.Application.DTOs
{
    public record CriarUsuarioResponse(
            string mensagem,
            Guid id,
            string nome,
            string email,
            string perfil,
            DateTime dataHoraCriacao
        );
}

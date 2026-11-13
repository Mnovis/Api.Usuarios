using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Usuarios.Application.DTOs
{
    public record CriarUsuarioRequest(
            string nome,
            string email,
            string senha
        );
}

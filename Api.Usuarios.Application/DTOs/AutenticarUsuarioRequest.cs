namespace Api.Usuarios.Application.DTOs
{
    public record AutenticarUsuarioRequest(
            string email,
            string senha
        );
}

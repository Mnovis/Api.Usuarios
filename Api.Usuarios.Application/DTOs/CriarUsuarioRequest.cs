namespace Api.Usuarios.Application.DTOs
{
    public record CriarUsuarioRequest(
            string nome,
            string email,
            string senha
        );
}

namespace Api.Usuarios.Application.DTOs
{
    public record AutenticarUsuarioResponse(
            Guid id,
            string nome,
            string email,
            string perfil,
            DateTime dataHoraAcesso,
            DateTime dataHoraExpiracao,
            string acessToken
        );
}

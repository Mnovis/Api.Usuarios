using Api.Usuarios.Domain.Entities;
using FluentValidation;

namespace Api.Usuarios.Domain.Validators
{
    public class PerfilValidator : AbstractValidator<Perfil>
    {
        // Método construtor para mapear as validações da entidade
        public PerfilValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O Nome do Perfil é obrigatório.")
                .MinimumLength(20).WithMessage("O Nome do Perfil deve ter no mínimo 20 caracteres.");
        }
    }
}

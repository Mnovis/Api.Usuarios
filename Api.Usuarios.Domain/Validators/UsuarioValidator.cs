using Api.Usuarios.Domain.Entities;
using FluentValidation;

namespace Api.Usuarios.Domain.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório.")
                .MinimumLength(6).WithMessage("O Nome deve ter no mínimo 6 caracteres.")
                .MaximumLength(150).WithMessage("O Nome deve ter no máximo 150 caracteres");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O Email é obrigatório.")
                .EmailAddress().WithMessage("Informe um endereço de email válido");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A Senha é obrigatória.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("A Senha deve conter pelo menos 1 letra minúscula, 1 letra maíuscula, 1 número,1 caractere especial e no mínimo 8 dígitos.");
        }
    }
}

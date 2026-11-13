using Api.Usuarios.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Usuarios.Domain.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("Informe o nome do usuário.")
                .MinimumLength(6).WithMessage("Informe no mínimo 6 caracteres.")
                .MaximumLength(150).WithMessage("Informe no máximo 150 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Informe o email do usuário.")
                .EmailAddress().WithMessage("Informe um endereço de email válido.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("Informe a senha do usuário.")
                .Matches(@"\A(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9\s])\S{8,}\z")
                .WithMessage("A senha deve conter pelo menos 1 letra minúscula, 1 letra maiúscula, 1 número, 1 caractere especial e no mínimo 8 digitos.");
        }
    }
}

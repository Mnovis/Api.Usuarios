using Api.Usuarios.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Usuarios.Domain.Validators
{
    public class PerfilValidator : AbstractValidator<Perfil>
    {
        //Método construtor para mapear as validações da entidade
        public PerfilValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("Informe o nome do perfil.")
                .MinimumLength(20).WithMessage("Informe no mínimo 20 caracteres.");
        }
    }
}

using API.ControleTarefas.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Nome de usuário é obrigatório!")
                .MaximumLength(250).WithMessage("Nome de usuário deve ter no máximo 250 caracteres!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatória!")
                .MaximumLength(512).WithMessage("Senha deve ter no máximo 512 caracteres!");
        }
    }
}

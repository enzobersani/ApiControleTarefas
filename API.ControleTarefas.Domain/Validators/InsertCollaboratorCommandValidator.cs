using API.ControleTarefas.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Validators
{
    public class InsertCollaboratorCommandValidator : AbstractValidator<InsertCollaboratorCommand>
    {
        public InsertCollaboratorCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome do colaborador é obrigatório!")
                .MaximumLength(250).WithMessage("Nome deve ter no máximo 250 caracteres!");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Código do usuário é obrigatório!");
        }
    }
}

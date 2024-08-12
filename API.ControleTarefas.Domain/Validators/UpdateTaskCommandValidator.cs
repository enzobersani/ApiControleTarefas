using API.ControleTarefas.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Validators
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id da tarefa é obrigatório.");

            RuleFor(x => x.Name)
                .MaximumLength(250).WithMessage("Nome da tarefa deve ter no máximo 250 caracteres");

            RuleFor(x => x.ProjectId)
                 .NotEqual(Guid.Empty).When(x => x.ProjectId.HasValue).WithMessage("Id do projeto não pode ser passado como vazio.");
        }
    }
}

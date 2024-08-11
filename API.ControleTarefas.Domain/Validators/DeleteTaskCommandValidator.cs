using API.ControleTarefas.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Validators
{
    public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id da tarefa é obrigatório!");
        }
    }
}

using API.ControleTarefas.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Validators
{
    public class InsertTimeTrackersCommandValidator : AbstractValidator<InsertTimeTrackersCommand>
    {
        public InsertTimeTrackersCommandValidator()
        {
            RuleFor(task => task.StartDate)
                .NotEmpty().WithMessage("A data de início é obrigatória.");

            RuleFor(task => task.EndDate)
                .NotEmpty().WithMessage("A data de término é obrigatória.")
                .GreaterThanOrEqualTo(task => task.StartDate)
                .WithMessage("A data de término deve ser maior ou igual à data de início.");

            RuleFor(task => task.TaskId)
                .NotEmpty().WithMessage("O código da tarefa é obrigatório.");

            RuleFor(task => task.CollaboratorId)
                .NotEmpty().WithMessage("O código do colaborador é obrigatório.");
        }
    }
}

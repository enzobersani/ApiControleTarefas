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
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("A data de início é obrigatória.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("A data de término é obrigatória.")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("A data de término deve ser maior ou igual à data de início.");

            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("O código da tarefa é obrigatório.");

            RuleFor(x => x.CollaboratorId)
                .NotEmpty().WithMessage("O código do colaborador é obrigatório.");

            RuleFor(x => x.TimeZoneId)
                .NotEmpty().WithMessage("Time zone é obrigatório.")
                .Must(ValidTimeZone).WithMessage("Time zone inválido.");
        }

        private bool ValidTimeZone(string timeZoneId)
        {
            return TimeZoneInfo.GetSystemTimeZones().Any(y => y.Id == timeZoneId);
        }
    }
}

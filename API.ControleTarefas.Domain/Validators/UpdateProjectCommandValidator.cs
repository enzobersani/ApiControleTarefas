using API.ControleTarefas.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Validators
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Código do projeto é obrigatório!");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome do projeto é obrigatório!")
                .MaximumLength(250).WithMessage("Nome deve ter no máximo 250 caracteres!");

        }
    }
}

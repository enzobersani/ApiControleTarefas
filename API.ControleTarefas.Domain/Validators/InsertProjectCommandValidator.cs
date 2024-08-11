using API.ControleTarefas.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Validators
{
    public class InsertProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public InsertProjectCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome do projeto é obrigatório!")
                .MaximumLength(250).WithMessage("Nome de usuário deve ter no máximo 250 caracteres!");
        }
    }
}

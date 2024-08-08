using API.ControleTarefas.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Validators
{
    public class InsertTaskCommandValidator : AbstractValidator<InsertTaskCommand>
    {
        public InsertTaskCommandValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome da tarefa é obrigatório!")
                .MaximumLength(250).WithMessage("Nome de usuário deve ter no máximo 250 caracteres!");
        }
    }
}

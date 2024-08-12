using API.ControleTarefas.Domain.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Validators
{
    public class SearchHoursQueryValidator : AbstractValidator<SearchHoursQuery>
    {
        public SearchHoursQueryValidator() 
        {
            RuleFor(x => x.CollaboratorId)
                .NotEmpty().WithMessage("É necessário informar o Id.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("É necessário informar uma data!");
        }
    }
}

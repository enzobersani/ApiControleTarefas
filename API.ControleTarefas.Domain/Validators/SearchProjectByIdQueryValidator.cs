using API.ControleTarefas.Domain.Queries;
using FluentValidation;

namespace API.ControleTarefas.Domain.Validators
{
    public class SearchProjectByIdQueryValidator : AbstractValidator<SearchProjectByIdQuery>
    {
        public SearchProjectByIdQueryValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("É necessário informar o Id.");
        }
    }
}

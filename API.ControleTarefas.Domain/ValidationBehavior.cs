using API.ControleTarefas.Domain.Notification;
using FluentValidation;
using MediatR;

namespace API.ControleTarefas.Domain
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> _validator;
        private readonly INotificationService _notifcations;

        public ValidationBehavior(IValidator<TRequest> validator, INotificationService notifications)
        {
            _validator = validator;
            _notifcations = notifications;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    _notifcations.AddNotification(error.PropertyName, error.ErrorMessage);
                }
                return default;
            }

            return await next();
        }
    }
}

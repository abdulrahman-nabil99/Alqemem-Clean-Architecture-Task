using FluentValidation;
using MediatR;
namespace CleanArchTask.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationBehavior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Resolve the validator for the current request type
            var validator = _serviceProvider.GetService(typeof(IValidator<TRequest>)) as IValidator<TRequest>;

            if (validator != null)  // If a validator exists, perform validation
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    // Collect validation errors and return an error response
                    var validationErrors = validationResult.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    // Return or throw validation failure
                    throw new ValidationException(string.Join(", ", validationErrors));
                }
            }

            // If validation passed or no validator is present, continue with the next handler
            return await next();
        }
    }
}

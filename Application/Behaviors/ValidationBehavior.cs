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
            var validator = _serviceProvider.GetService(typeof(IValidator<TRequest>)) as IValidator<TRequest>;

            if (validator != null)  
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {         
                    var validationErrors = validationResult.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    throw new ValidationException(string.Join(", ", validationErrors));
                }
            }
            return await next();
        }
    }
}

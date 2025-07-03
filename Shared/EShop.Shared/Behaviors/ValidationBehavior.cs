using FluentValidation;

namespace EShop.Shared.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, ICommand<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            //var failures = validators
            //    .Select(v => v.Validate(context))
            //    .SelectMany(result => result.Errors)
            //    .Where(f => f != null)
            //    .ToList();

            var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)).
                Where(x => x != null));

            var failures = validationResults.Where(x => x.Errors.Any())
                .SelectMany(x => x.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                // var errorMessages = string.Join(", ", failures.Select(f => f.ErrorMessage));
                throw new ValidationException($"Validation failed: {failures.FirstOrDefault()?.ErrorMessage}", errors: failures);
            }

            return await next();
        }
    }
}

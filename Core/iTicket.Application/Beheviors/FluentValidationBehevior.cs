using FluentValidation;
using MediatR;

namespace iTicket.Application.Beheviors
{
    public class FluentValidationBehevior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validator)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validator = validator;

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failtures = validator
                    .Select(v => v.Validate(context))
                    .SelectMany(v => v.Errors)
                    .GroupBy(x => x.ErrorMessage)
                    .Select(x => x.First())
                    .Where(x => x != null)
                    .ToList();

            if (failtures.Count != 0)
                throw new ValidationException(failtures);


            return next();
        }
    }
}

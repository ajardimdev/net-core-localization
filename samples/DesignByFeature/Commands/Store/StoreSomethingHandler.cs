using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace DesignByFeature.Commands.Store
{
    public class StoreSomethingHandler : IRequestHandler<StoreSomething, StoreSomethingResponse>
    {
        private readonly IValidator<StoreSomething> _specification;

        public StoreSomethingHandler(IValidator<StoreSomething> specification)
        {
            _specification = specification;
        }
        public Task<StoreSomethingResponse> Handle(StoreSomething request, CancellationToken cancellationToken)
        {
            var validationResult = _specification.Validate(request);
            if (!validationResult.IsValid)
                return Task.FromResult(new StoreSomethingResponse(validationResult));

            return Task.FromResult(new StoreSomethingResponse
            {
                DecimalValue = request.DecimalValue,
                StringValue = request.StringValue,
                GuidValue = request.GuidValue,
                IntValue = request.IntValue
            });
        }
    }
}
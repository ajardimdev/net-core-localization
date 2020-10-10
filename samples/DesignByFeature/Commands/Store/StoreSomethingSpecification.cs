using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace DesignByFeature.Commands.Store
{
    public class StoreSomethingSpecification : AbstractValidator<StoreSomething>
    {
        public StoreSomethingSpecification(IStringLocalizer<StoreSomethingSpecification> localizer)
        {
            RuleFor(_ => _.GuidValue).NotEqual(default(Guid))
                .WithMessage(localizer["GuidValue is required"].Value);

            RuleFor(_ => _.StringValue).NotEmpty()
                .WithMessage(localizer["StringValue is required"].Value);

            RuleFor(_ => _.DecimalValue).NotEmpty()
                .WithMessage(localizer["DecimalValue is required"].Value);

            RuleFor(_ => _.IntValue).NotEmpty()
                .WithMessage(localizer["IntValue is required"].Value);
        }
    }
}
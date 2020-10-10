using System;
using FluentValidation.Results;

namespace DesignByFeature.Commands.Store
{
    public class StoreSomethingResponse
    {
        public StoreSomethingResponse(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }

        public StoreSomethingResponse()
        {
        }
        public ValidationResult ValidationResult { get; set; }

        public Guid GuidValue { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public decimal DecimalValue { get; set; }
    }
}
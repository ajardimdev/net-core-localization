using System;
using MediatR;

namespace DesignByFeature.Commands.Store
{
    public class StoreSomething : IRequest<StoreSomethingResponse>
    {
        public Guid GuidValue { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public decimal DecimalValue { get; set; }
    }
}
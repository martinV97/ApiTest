using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebService.Core.Entities
{
    public class PaymentDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class PaymentDTOValidator : AbstractValidator<PaymentDTO>
    {
        public PaymentDTOValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(payment => payment.Id)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(payment => payment.Name)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches("[^a-zA-Z0-9]*")
                .WithMessage("The {PropertyName} only can have letter or numbers");
        }
    }
}

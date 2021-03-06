using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebService.Core.Entities
{
    public class Product
    {
        public string Name { get; set; }
        public string Details { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches("[^a-zA-Z0-9]*")
                .WithMessage("The {PropertyName} only can have letter or numbers");
            RuleFor(product => product.Details)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches("[^a-zA-Z0-9]*")
                .WithMessage("The {PropertyName} only can have letter or numbers");
            RuleFor(product => product.CategoryId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
        }
    }
}

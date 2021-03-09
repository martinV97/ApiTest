using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WebService.Core.Enumerations;

namespace WebService.Core.Entities
{
    public class Offer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string PublicationId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public decimal OfferMoney { get; set; }
        public string DetailOffer { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string PaymentId { get; set; }
        public OfferStateEnum State { get; set; }
    }

    public class OfferValidator : AbstractValidator<Offer>
    {
        public OfferValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(offer => offer.PublicationId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(offer => offer.UserId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(offer => offer.State)
                .Must(state => Enum.IsDefined(typeof(OfferStateEnum), state))
                .WithMessage("The {PropertyName} must be a valid OfferStateEnum");
        }
    }
}

using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WebService.Core.Enumerations;

namespace WebService.Core.Entities
{
    public class OfferDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
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

    public class OfferDTOValidator : AbstractValidator<OfferDTO>
    {
        public OfferDTOValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(offer => offer.Id)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(offer => offer.PublicationId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(offer => offer.UserId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(offer => offer.State)
                .Must(state => Enum.IsDefined(typeof(PlaceTypeEnum), state))
                .WithMessage("The {PropertyName} must be a valid OfferStateEnum");
        }
    }
}

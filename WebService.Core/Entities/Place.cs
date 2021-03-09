using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WebService.Core.Enumerations;

namespace WebService.Core.Entities
{
    public class Place
    {
        public string Name { get; set; }
        public PlaceTypeEnum Type { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string PrincipalPlaceId { get; set; }
    }

    public class PlaceValidator : AbstractValidator<Place>
    {
        public PlaceValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(place => place.Name)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches("[a-zA-z]*")
                .WithMessage("The {PropertyName} only can have letter");
            RuleFor(place => place.Type)
                .Must(type => Enum.IsDefined(typeof(PlaceTypeEnum), type))
                .WithMessage("The {PropertyName} must be a valid PlaceTypeEnum");
        }
    }
}

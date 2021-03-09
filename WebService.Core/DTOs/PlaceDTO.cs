using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using WebService.Core.Enumerations;

namespace WebService.Core.DTOs
{
    public class PlaceDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public PlaceTypeEnum Type { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string PrincipalPlaceId { get; set; }
    }

    public class PlaceDTOValidator : AbstractValidator<PlaceDTO>
    {
        public PlaceDTOValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(place => place.Id)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(place => place.Name)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches("[^a-zA-Z0-9]*")
                .WithMessage("The {PropertyName} only can have letter or numbers");
            RuleFor(place => place.Type)
                .Must(type => Enum.IsDefined(typeof(PlaceTypeEnum), type))
                .WithMessage("The {PropertyName} must be a valid PlaceDTOTypeEnum");
        }
    }
}

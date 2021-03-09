using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using WebService.Core.Enumerations;

namespace WebService.Core.Entities
{
    public class PublicationDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Pictures { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PublicationStateEnum State { get; set; }
    }

    public class PublicationDTOValidator : AbstractValidator<PublicationDTO>
    {
        public PublicationDTOValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(publication => publication.Id)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(publication => publication.UserId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(publication => publication.ProductId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(publication => publication.Title)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("The {PropertyName} only can have letter or numbers");
            RuleFor(publication => publication.Description)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(publication => publication.State)
                .Must(state => Enum.IsDefined(typeof(PublicationStateEnum), state))
                .WithMessage("The {PropertyName} must be a valid PublicationStateEnum");
        }
    }
}

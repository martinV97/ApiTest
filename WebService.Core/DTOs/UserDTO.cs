using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using WebService.Core.Enumerations;

namespace WebService.Core.Entities
{
    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public GenderEnum Gender { get; set; }
        public UserStateEnum State { get; set; }
        public string AccountBank { get; set; } 
        public decimal? Reputation { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string PlaceId { get; set; }
    }

    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(publication => publication.Id)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(publication => publication.Identification)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches(@"^[0-9]+$")
                .WithMessage("The {PropertyName} only can have numbers");
            RuleFor(publication => publication.Name)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches(@"^[a-zA-Z]+$")
                .WithMessage("The {PropertyName} only can have letters");
            RuleFor(publication => publication.LastName)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches(@"^[a-zA-Z]+$")
                .WithMessage("The {PropertyName} only can have letters");
            RuleFor(publication => publication.Email)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .EmailAddress()
                .WithMessage("The {PropertyName} must be a valid Email");
            RuleFor(publication => publication.Password)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .MinimumLength(10)
                .WithMessage("The {PropertyName} minimum size is 10 characteres");
            RuleFor(publication => publication.Gender)
                .Must(state => Enum.IsDefined(typeof(GenderEnum), state))
                .WithMessage("The {PropertyName} must be a valid GenderEnum");
            RuleFor(publication => publication.State)
                .Must(state => Enum.IsDefined(typeof(UserStateEnum), state))
                .WithMessage("The {PropertyName} must be a valid UserStateEnum");
            RuleFor(publication => publication.PlaceId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
        }
    }
}

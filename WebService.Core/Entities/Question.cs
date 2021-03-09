using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebService.Core.Entities
{
    public class Question
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string PublicationId { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public DateTime QuestionCreated { get; set; }
        public bool State { get; set; }
    }

    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(publication => publication.UserId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(publication => publication.PublicationId)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null");
            RuleFor(question => question.Text)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches("[^a-zA-Z0-9]*")
                .WithMessage("The {PropertyName} only can have letter or numbers");
        }
    }
}

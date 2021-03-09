using FluentValidation;

namespace WebService.Core.Entities
{
    public class Category
    {
        public string Name { get; set; }
    }

    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(customer => customer.Name)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches("[^a-zA-Z0-9]*")
                .WithMessage("The {PropertyName} only can have letter or numbers");
        }
    }
}

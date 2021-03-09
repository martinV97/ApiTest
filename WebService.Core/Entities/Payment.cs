using FluentValidation;

namespace WebService.Core.Entities
{
    public class Payment
    {
        public string Name { get; set; }
    }

    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(payment => payment.Name)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty or null")
                .Matches("[^a-zA-Z0-9]*")
                .WithMessage("The {PropertyName} only can have letter or numbers");
        }
    }
}

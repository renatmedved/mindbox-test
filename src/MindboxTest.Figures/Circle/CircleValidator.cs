using FluentValidation;

namespace MindboxTest.Figures.Circle
{
    public sealed class CircleValidator : AbstractValidator<CircleDescription>
    {
        public CircleValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Radius).GreaterThanOrEqualTo(0d);
        }
    }
}

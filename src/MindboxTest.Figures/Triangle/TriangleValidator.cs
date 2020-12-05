using FluentValidation;

namespace MindboxTest.Figures.Triangle
{
    public sealed class TriangleValidator : AbstractValidator<TriangleDescription>
    {
        public TriangleValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Height).GreaterThanOrEqualTo(0d);
            RuleFor(x => x.Base).GreaterThanOrEqualTo(0d);
        }
    }
}

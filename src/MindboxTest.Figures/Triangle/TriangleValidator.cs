using FluentValidation;

namespace MindboxTest.Figures.Triangle
{
    public sealed class TriangleValidator : AbstractValidator<TriangleDescription>
    {
        public TriangleValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Height).GreaterThan(0d);
            RuleFor(x => x.Base).GreaterThan(0d);
        }
    }
}

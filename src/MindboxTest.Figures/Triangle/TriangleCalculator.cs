using MindboxTest.Figures.Base;

namespace MindboxTest.Figures.Triangle
{
    public sealed class TriangleCalculator : ValidatedCalculator<TriangleDescription>
    {
        public TriangleCalculator(TriangleValidator validator):base(validator)
        {
        }

        protected override double CalculateLogic(TriangleDescription desc)
        {
            return 0.5 * desc.Height * desc.Base;
        }
    }
}

using MindboxTest.Figures.Base;

namespace MindboxTest.Figures.Triangle
{
    public sealed class TriangleCalculator: IAreaCalculator<TriangleDescription>
    {
        public double Calculate(TriangleDescription desc)
        {
            return 0.5 * desc.Height * desc.Base;
        }
    }
}

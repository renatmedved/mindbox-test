using MindboxTest.Figures.Base;

namespace MindboxTest.Figures.Triangle
{
    public sealed class TriangleDescription : IFigureDescription
    {
        public double Height { get; set; }
        public double Base { get; set; }
    }
}

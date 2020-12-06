using MindboxTest.Figures.Circle;
using MindboxTest.Figures.Triangle;

namespace MindboxTest.Figures.Proxy
{
    public static class ProxyFigureInitializator
    {
        public static void Initialize()
        {
            ProxyFigureStorage.AddFigureProcessors(new CircleValidator(), new CircleCalculator(), "circle");
            ProxyFigureStorage.AddFigureProcessors(new TriangleValidator(), new TriangleCalculator(), "triangle");
        }
    }
}

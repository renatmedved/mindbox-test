using MindboxTest.Contracts;

namespace MindboxTest.Figures.Base
{
    public interface IAreaCalculator<TFigureDescription>
    {
        Result<double> Calculate(TFigureDescription desc);
    }
}

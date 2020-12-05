using MindboxTest.Contracts.Results;

namespace MindboxTest.Figures.Base
{
    public interface IAreaCalculator<TFigureDescription>
    {
        Result<double> Calculate(TFigureDescription desc);
    }
}

namespace MindboxTest.Figures.Base
{
    public interface IAreaCalculator<TFigureDescription>
        where TFigureDescription : IFigureDescription
    {
        double Calculate(TFigureDescription desc);
    }
}

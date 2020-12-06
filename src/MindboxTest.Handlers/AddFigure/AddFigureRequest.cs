using MediatR;

using MindboxTest.Contracts.Results;
using MindboxTest.Figures.Base;

namespace MindboxTest.Handlers.AddFigure
{
    public class AddFigureRequest : IRequest<Result<AddFigureResponseData>>
    {
        public string FigureType { get; set; }
        public IFigureDescription FigureDescription { get; set; }
    }
}

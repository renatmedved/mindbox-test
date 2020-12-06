using MediatR;

using MindboxTest.Contracts.Results;

namespace MindboxTest.Handlers.CalcArea
{
    public class CalcAreaRequest:IRequest<Result<CalcAreaResponseData>>
    {
        public long FigureId { get; set; }
    }
}

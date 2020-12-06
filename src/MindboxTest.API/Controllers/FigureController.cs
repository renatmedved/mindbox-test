using MediatR;

using Microsoft.AspNetCore.Mvc;

using MindboxTest.API.Responses;
using MindboxTest.Contracts.Results;
using MindboxTest.Handlers.AddFigure;
using MindboxTest.Handlers.CalcArea;

using System.Threading.Tasks;

namespace MindboxTest.API.Controllers
{
    public class FigureController:Controller
    {
        private readonly IMediator _mediator;

        public FigureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/figure")]
        public async Task<ApiResponse<AddFigureResponseData>> AddFigure(
            [FromBody]AddFigureRequest request)
        {
            Result<AddFigureResponseData> response = await _mediator.Send(request);

            return new ApiResponse<AddFigureResponseData>(response);
        }

        [HttpGet("/figure/{id}")]
        public async Task<ApiResponse<CalcAreaResponseData>> CalcArea(long id)
        {
            var response = await _mediator.Send(new CalcAreaRequest { FigureId = id });

            return new ApiResponse<CalcAreaResponseData>(response);
        }
    }
}

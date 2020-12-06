using MediatR;

using MindboxTest.Contracts.Results;
using MindboxTest.DAL.QueryFactory;
using MindboxTest.DAL.Tables;
using MindboxTest.Figures.Base;
using MindboxTest.Figures.Proxy;

using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MindboxTest.Handlers.AddFigure
{
    public class AddFigureHandler : IRequestHandler<AddFigureRequest, Result<AddFigureResponseData>>
    {
        private readonly IQueryFactory _queryFactory;
        private readonly ProxyFigureValidator _validator;

        public AddFigureHandler(IQueryFactory queryFactory, ProxyFigureValidator validator, ProxyFigureDescriptionProvider descriptionProvider)
        {
            _queryFactory = queryFactory;
            _validator = validator;
        }

        public async Task<Result<AddFigureResponseData>> Handle(AddFigureRequest request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(request.FigureType) || request.FigureDescription == null)
            {
                return Result<AddFigureResponseData>.MakeFailMessage("error figure");
            }

            IFigureDescription description = request.FigureDescription;

            Result<Empty> result = _validator.Validate(description);

            if (result.Fail)
            {
                return Result<AddFigureResponseData>.MakeFail(result.Errors);
            }

            long figureId = await _queryFactory.SaveFigure(new Figure
            {
                Description = JsonConvert.SerializeObject(request.FigureDescription),
                Type = request.FigureType
            });

            var responseData = new AddFigureResponseData { FigureId = figureId };

            return Result<AddFigureResponseData>.MakeSucces(responseData);
        }
    }
}

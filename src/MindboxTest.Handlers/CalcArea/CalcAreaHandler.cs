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

namespace MindboxTest.Handlers.CalcArea
{
    public class CalcAreaHandler : IRequestHandler<CalcAreaRequest, Result<CalcAreaResponseData>>
    {
        private readonly IQueryFactory _queryFactory;
        private readonly ProxyFigureCalculator _figureCalculator;
        private readonly ProxyFigureDescriptionProvider _descriptionProvider;

        public CalcAreaHandler(IQueryFactory queryFactory, ProxyFigureCalculator figureCalculator, ProxyFigureDescriptionProvider descriptionProvider)
        {
            _queryFactory = queryFactory;
            _figureCalculator = figureCalculator;
            _descriptionProvider = descriptionProvider;
        }
        public async Task<Result<CalcAreaResponseData>> Handle(CalcAreaRequest request, CancellationToken cancellationToken)
        {
            Figure dbFigure = await _queryFactory.RetrieveFigure(request.FigureId);

            if (dbFigure == null)
            {
                return Result<CalcAreaResponseData>.MakeFailMessage("figure not found");
            }

            Result<Type> descType = _descriptionProvider.GetDescriptionType(dbFigure.Type);

            if (descType.Fail)
            {
                return Result<CalcAreaResponseData>.MakeFailMessage("figure not found");
            }

            if (!(JsonConvert.DeserializeObject(dbFigure.Description, descType.Data) 
                is IFigureDescription description))
            {
                return Result<CalcAreaResponseData>.MakeFailMessage("figure not found");
            }

            Result<double> result = _figureCalculator.Calculate(description);

            if (result.Success)
            {
                return Result<CalcAreaResponseData>.MakeSucces(new CalcAreaResponseData { Area = result.Data });
            }
            else
            {
                return Result<CalcAreaResponseData>.MakeFail(result.Errors);
            }
        }
    }
}

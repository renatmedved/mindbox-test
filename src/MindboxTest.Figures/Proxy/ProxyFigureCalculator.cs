using MindboxTest.Contracts.Results;
using MindboxTest.Figures.Base;

namespace MindboxTest.Figures.Proxy
{
    public class ProxyFigureCalculator
    {
        private readonly ProxyFigureStorage _storage;

        public ProxyFigureCalculator(ProxyFigureStorage storage)
        {
            _storage = storage;
        }

        public Result<double> Calculate(IFigureDescription request)
        {
            var processor = _storage.GetProxyFigureProcessor(request);

            if (processor == null)
            {
                return Result<double>.MakeFailMessage("error figure");
            }

            Result<Empty> validateResult = processor.Validate(request);

            if (validateResult.Fail)
            {
                return Result<double>.MakeFail(validateResult.Errors);
            }

            double result = processor.Calculate(request);

            return Result<double>.MakeSucces(result);
        }
    }
}

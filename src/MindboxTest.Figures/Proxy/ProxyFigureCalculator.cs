using MindboxTest.Contracts.Results;
using MindboxTest.Figures.Base;

namespace MindboxTest.Figures.Proxy
{
    public class ProxyFigureCalculator
    {
        private readonly ProxyFigureStorage _storage;
        private readonly ProxyFigureValidator _validator;

        public ProxyFigureCalculator(ProxyFigureStorage storage, ProxyFigureValidator validator)
        {
            _storage = storage;
            _validator = validator;
        }

        public Result<double> Calculate(IFigureDescription request)
        {
            var processor = _storage.GetProxyFigureProcessor(request);

            if (processor == null)
            {
                return Result<double>.MakeFailMessage("error figure");
            }

            Result<Empty> validateResult = _validator.Validate(request);

            if (validateResult.Fail)
            {
                return Result<double>.MakeFail(validateResult.Errors);
            }

            double result = processor.Calculate(request);

            return Result<double>.MakeSucces(result);
        }
    }
}

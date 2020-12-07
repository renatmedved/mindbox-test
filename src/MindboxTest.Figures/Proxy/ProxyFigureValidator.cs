using FluentValidation.Results;

using MindboxTest.Contracts.Results;
using MindboxTest.Figures.Base;
using MindboxTest.Infrastructure;

namespace MindboxTest.Figures.Proxy
{
    public class ProxyFigureValidator
    {
        private readonly ProxyFigureStorage _storage;

        public ProxyFigureValidator(ProxyFigureStorage storage)
        {
            _storage = storage;
        }

        public Result<Empty> Validate(IFigureDescription request)
        {
            ProxyFigureProcessors processor = _storage.GetProxyFigureProcessor(request);

            if (processor == null)
            {
                return Result<Empty>.MakeFailMessage("error figure");
            }

            ValidationResult result = processor.Validate(request);

            if (result.IsValid)
            {
                return Result<Empty>.MakeSucces(Empty.Instance);
            }

            return Result<Empty>.MakeFail(result.ErrorsToListString());
        }
    }
}

using MindboxTest.Contracts.Results;
using MindboxTest.Figures.Base;

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
            var processor = _storage.GetProxyFigureProcessor(request);

            if (processor == null)
            {
                return Result<Empty>.MakeFailMessage("error figure");
            }

            return processor.Validate(request);
        }
    }
}

using MindboxTest.Contracts.Results;

using System;

namespace MindboxTest.Figures.Proxy
{
    public class ProxyFigureDescriptionProvider
    {
        private readonly ProxyFigureStorage _storage;

        public ProxyFigureDescriptionProvider(ProxyFigureStorage storage)
        {
            _storage = storage;
        }

        public Result<Type> GetDescriptionType(string type)
        {
            Type descriptionType = _storage.GetDescriptionType(type);

            if (descriptionType == null)
            {
                return Result<Type>.MakeFailMessage("figure not found");
            }

            return Result<Type>.MakeSucces(descriptionType);
        }
    }
}

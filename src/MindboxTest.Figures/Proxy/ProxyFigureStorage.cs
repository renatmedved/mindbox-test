using FluentValidation;

using MindboxTest.Figures.Base;

using System;
using System.Collections.Concurrent;

namespace MindboxTest.Figures.Proxy
{
    public class ProxyFigureStorage
    {
        private static readonly ConcurrentDictionary<Type, ProxyFigureProcessors> _typeMap = new ConcurrentDictionary<Type, ProxyFigureProcessors>();
        private static readonly ConcurrentDictionary<string, Type> _codeMap = new ConcurrentDictionary<string, Type>();

        internal static void AddFigureProcessors<TDescription>(
                                AbstractValidator<TDescription> validator, 
                                IAreaCalculator<TDescription> calculator,
                                string code)

            where TDescription:IFigureDescription
        {
            var processors = new ProxyFigureProcessors();
            processors.Init(validator, calculator);

            var type = typeof(TDescription);

            _typeMap.AddOrUpdate(type, processors, (key, old) => processors);
            _codeMap.AddOrUpdate(code, type, (key, old)=> type);
        }

        /// <summary>
        /// Dangerous: not thread-safe
        /// </summary>
        internal static void ClearAllFigureProcessors()
        {
            _typeMap.Clear();
            _codeMap.Clear();
        }

        internal virtual Type GetDescriptionType(string type)
        {
            if (_codeMap.TryGetValue(type, out Type value))
            {
                return value;
            }

            return null;
        }

        internal virtual ProxyFigureProcessors GetProxyFigureProcessor(IFigureDescription description)
        {
            if (_typeMap.TryGetValue(description.GetType(), out ProxyFigureProcessors value))
            {
                return value;
            }

            return null;
        }
    }
}

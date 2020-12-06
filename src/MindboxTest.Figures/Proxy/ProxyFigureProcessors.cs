using FluentValidation;
using FluentValidation.Results;
using MindboxTest.Contracts.Results;
using MindboxTest.Figures.Base;
using MindboxTest.Infrastructure;

using System;

namespace MindboxTest.Figures.Proxy
{
    internal sealed class ProxyFigureProcessors
    {
        public Func<IFigureDescription, double> Calculate { get; private set; }
        public Func<IFigureDescription, Result<Empty>> Validate { get; private set; }

        public void Init<TDescription>(AbstractValidator<TDescription> validator, 
            IAreaCalculator<TDescription> calculator)
            where TDescription : IFigureDescription
        {
            Calculate = (IFigureDescription desc) => calculator.Calculate((TDescription)desc);
            Validate = (IFigureDescription desc) =>
            {
                ValidationResult result = validator.Validate((TDescription)desc);

                if (result.IsValid)
                {
                    return Result<Empty>.MakeSucces(Empty.Instance);
                }

                return Result<Empty>.MakeFail(result.ErrorsToListString());
            };
        }
    }
}

using FluentValidation;
using FluentValidation.Results;

using MindboxTest.Figures.Base;

using System;

namespace MindboxTest.Figures.Proxy
{
    internal sealed class ProxyFigureProcessors
    {
        public Func<IFigureDescription, double> Calculate { get; private set; }
        public Func<IFigureDescription, ValidationResult> Validate { get; private set; }

        public void Init<TDescription>(AbstractValidator<TDescription> validator, 
            IAreaCalculator<TDescription> calculator)
            where TDescription : IFigureDescription
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            if (calculator == null)
            {
                throw new ArgumentNullException(nameof(calculator));
            }

            Calculate = (IFigureDescription desc) => calculator.Calculate((TDescription)desc);
            Validate = (IFigureDescription desc) => validator.Validate((TDescription)desc);
        }
    }
}

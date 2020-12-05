using FluentValidation;
using FluentValidation.Results;
using MindboxTest.Contracts.Results;
using System.Collections.Generic;
using System.Linq;

namespace MindboxTest.Figures.Base
{
    public abstract class ValidatedCalculator<TFigureDescription> : IAreaCalculator<TFigureDescription>
    {
        private readonly AbstractValidator<TFigureDescription> _validator;

        public ValidatedCalculator(AbstractValidator<TFigureDescription> validator)
        {
            _validator = validator;
        }

        public Result<double> Calculate(TFigureDescription desc)
        {
            ValidationResult validation = _validator.Validate(desc);

            if (!validation.IsValid)
            {
                IEnumerable<string> errs = validation.Errors.Select(x => x.ErrorMessage);

                return Result<double>.MakeFail(errs);
            }

            double answer = CalculateLogic(desc);

            return Result<double>.MakeSucces(answer);
        }

        protected abstract double CalculateLogic(TFigureDescription desc);
    }
}

using MindboxTest.Figures.Base;
using System;

namespace MindboxTest.Figures.Circle
{
    public sealed class CircleCalculator : ValidatedCalculator<CircleDescription>
    {
        public CircleCalculator(CircleValidator validator):base(validator)
        {
        }

        protected override double CalculateLogic(CircleDescription desc)
        {
            return desc.Radius * desc.Radius * Math.PI;
        }
    }
}

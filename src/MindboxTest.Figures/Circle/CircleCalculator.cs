using MindboxTest.Figures.Base;

using System;

namespace MindboxTest.Figures.Circle
{
    public sealed class CircleCalculator : IAreaCalculator<CircleDescription>
    {
        public double Calculate(CircleDescription desc)
        {
            return desc.Radius * desc.Radius * Math.PI;
        }
    }
}

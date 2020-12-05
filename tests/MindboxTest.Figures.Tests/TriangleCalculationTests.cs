using MindboxTest.Figures.Triangle;
using NUnit.Framework;
using System;

namespace MindboxTest.Figures.Tests
{
    public class TriangleCalculationTests
    {
        private readonly TriangleCalculator _calc = new TriangleCalculator(new TriangleValidator());
        private readonly double _tolerance = 1.0 / Math.Pow(10, 10);

        [Test]
        public void TestThatAreaDivideBaseMultiply2IsHeight()
        {
            var rand = new Random();

            for(int i = 0; i < 100; i++)
            {
                double @base = rand.NextDouble() * 1000 + 1;//> 0
                double height = rand.NextDouble() * 1000 + 1;//> 0

                var triangleDesc = new TriangleDescription { Base = @base, Height = height };

                double result = _calc.Calculate(triangleDesc).Data;

                double actual = result / @base * 2.0;

                Assert.That(actual, Is.EqualTo(height).Within(_tolerance));
            }
        }
    }
}
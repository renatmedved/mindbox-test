using MindboxTest.Figures.Circle;
using NUnit.Framework;
using System;

namespace MindboxTest.Figures.Tests
{
    public class CircleCalculationTests
    {
        private readonly CircleCalculator _calc = new CircleCalculator(new CircleValidator());
        private readonly double _tolerance = 1.0 / Math.Pow(10, 10);

        [Test]
        public void TestThatAreaDividePiDivideRadiusIsRaduis()
        {
            var rand = new Random();

            for(int i = 0; i < 100; i++)
            {
                double radius = rand.NextDouble() * 1000 + 1;//> 0

                var circleDesc = new CircleDescription { Radius = radius };

                double result = _calc.Calculate(circleDesc).Data;

                double actual = result / Math.PI / radius;

                Assert.That(actual, Is.EqualTo(radius).Within(_tolerance));
            }
        }
    }
}
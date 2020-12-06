using MindboxTest.Figures.Circle;
using MindboxTest.TestHelpers;

using NUnit.Framework;

using System;

namespace MindboxTest.Figures.Tests
{
    public class CircleCalculationTests
    {
        private readonly CircleCalculator _calc = new CircleCalculator();

        [Test]
        public void TestThatAreaDividePiDivideRadiusIsRaduis()
        {
            var rand = new Random();

            for(int i = 0; i < 100; i++)
            {
                double radius = rand.NextDouble() * 1000 + 1;//> 0

                var circleDesc = new CircleDescription { Radius = radius };

                double result = _calc.Calculate(circleDesc);

                double actual = result / Math.PI / radius;

                Assert.That(actual, Is.EqualTo(radius).Within(DoubleHelpers.Tolerance));
            }
        }
    }
}
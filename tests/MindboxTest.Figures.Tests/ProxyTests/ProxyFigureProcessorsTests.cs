using FluentValidation.Results;

using MindboxTest.Figures.Circle;
using MindboxTest.Figures.Proxy;
using MindboxTest.Figures.Triangle;
using MindboxTest.TestHelpers;

using NUnit.Framework;

using System;

namespace MindboxTest.Figures.Tests.ProxyTests
{
    public class ProxyFigureProcessorsTests
    {
        [Test]
        public void PassNullValidatorOnInit_Throw()
        {
            var processor = new ProxyFigureProcessors();

            Assert.Throws<ArgumentNullException>(() => processor.Init(null, new CircleCalculator()));
        }

        [Test]
        public void PassNullCalculatorOnInit_Throw()
        {
            var processor = new ProxyFigureProcessors();

            Assert.Throws<ArgumentNullException>(() => processor.Init(new CircleValidator(), null));
        }

        [Test]
        public void ValidateValidData_Success()
        {
            var processor = new ProxyFigureProcessors();

            processor.Init(new CircleValidator(), new CircleCalculator());

            ValidationResult result = processor.Validate(new CircleDescription { Radius = 1 });

            Assert.True(result.IsValid);
        }

        [Test]
        public void ValidateNoValidData_Fail()
        {
            var processor = new ProxyFigureProcessors();

            processor.Init(new CircleValidator(), new CircleCalculator());

            ValidationResult result = processor.Validate(new CircleDescription { Radius = 0 });

            Assert.False(result.IsValid);
        }

        [Test]
        public void ValidateErrorTypeData_Fail()
        {
            var processor = new ProxyFigureProcessors();

            processor.Init(new CircleValidator(), new CircleCalculator());

            Assert.Throws<InvalidCastException>(() => processor.Validate(new TriangleDescription()));
        }

        [Test]
        public void Calculate_Success()
        {
            var processor = new ProxyFigureProcessors();

            processor.Init(new CircleValidator(), new CircleCalculator());

            double result = processor.Calculate(new CircleDescription { Radius = 1 });

            Assert.That(Math.PI, Is.EqualTo(result).Within(DoubleHelpers.Tolerance));
        }

        [Test]
        public void CalculateErrorTypeData_Fail()
        {
            var processor = new ProxyFigureProcessors();

            processor.Init(new CircleValidator(), new CircleCalculator());

            Assert.Throws<InvalidCastException>(() => processor.Calculate(new TriangleDescription()));
        }
    }
}

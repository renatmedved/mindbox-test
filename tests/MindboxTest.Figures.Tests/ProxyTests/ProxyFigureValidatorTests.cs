using MindboxTest.Contracts.Results;
using MindboxTest.Figures.Base;
using MindboxTest.Figures.Circle;
using MindboxTest.Figures.Proxy;

using Moq;

using NUnit.Framework;

namespace MindboxTest.Figures.Tests.ProxyTests
{
    public class ProxyFigureValidatorTests
    {
        [Test]
        public void StorageReturnNull_ReturnFail()
        {
            var storageMock = new Mock<ProxyFigureStorage>();

            storageMock
                .Setup(x => x.GetProxyFigureProcessor(It.IsAny<IFigureDescription>()))
                .Returns((ProxyFigureProcessors)null);

            var validator = new ProxyFigureValidator(storageMock.Object);

            Result<Empty> result = validator.Validate(new CircleDescription { Radius = 1 });

            Assert.AreEqual(result.Fail, true);
        }

        [Test]
        public void NoValidDescription_ReturnFail()
        {
            var processor = new ProxyFigureProcessors();
            processor.Init(new CircleValidator(), new CircleCalculator());

            var storageMock = new Mock<ProxyFigureStorage>();

            storageMock
                .Setup(x => x.GetProxyFigureProcessor(It.IsAny<IFigureDescription>()))
                .Returns(processor);

            var validator = new ProxyFigureValidator(storageMock.Object);

            Result<Empty> result = validator.Validate(new CircleDescription { Radius = 0 });

            Assert.AreEqual(result.Fail, true);
        }

        [Test]
        public void ValidDescription_ReturnSuccess()
        {
            var processor = new ProxyFigureProcessors();
            processor.Init(new CircleValidator(), new CircleCalculator());

            var storageMock = new Mock<ProxyFigureStorage>();

            storageMock
                .Setup(x => x.GetProxyFigureProcessor(It.IsAny<IFigureDescription>()))
                .Returns(processor);

            var validator = new ProxyFigureValidator(storageMock.Object);

            Result<Empty> result = validator.Validate(new CircleDescription { Radius = 1 });

            Assert.AreEqual(result.Success, true);
        }
    }
}

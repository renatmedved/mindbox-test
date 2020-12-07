using MindboxTest.Contracts.Results;
using MindboxTest.Figures.Circle;
using MindboxTest.Figures.Proxy;

using Moq;

using NUnit.Framework;

using System;

namespace MindboxTest.Figures.Tests.ProxyTests
{
    public class ProxyFigureDescriptionProviderTests
    {
        [Test]
        public void StorageReturnNull_ReturnFail()
        {
            var storageMock = new Mock<ProxyFigureStorage>();

            storageMock
                .Setup(x => x.GetDescriptionType(It.IsAny<string>()))
                .Returns((Type)null);

            var provider = new ProxyFigureDescriptionProvider(storageMock.Object);

            Result<Type> result = provider.GetDescriptionType("");

            Assert.AreEqual(result.Fail, true);
        }

        [Test]
        public void StorageReturnNotNull_ReturnSuccess()
        {
            var storageMock = new Mock<ProxyFigureStorage>();

            storageMock
                .Setup(x => x.GetDescriptionType(It.IsAny<string>()))
                .Returns(typeof(CircleDescription));

            var provider = new ProxyFigureDescriptionProvider(storageMock.Object);

            Result<Type> result = provider.GetDescriptionType("");

            Assert.AreEqual(result.Success, true);
        }
    }
}

using MindboxTest.Contracts.Results;
using MindboxTest.DAL.QueryFactory;
using MindboxTest.DAL.Tables;
using MindboxTest.Figures.Circle;
using MindboxTest.Figures.Proxy;
using MindboxTest.Handlers.AddFigure;

using Moq;

using NUnit.Framework;

using System.Threading;
using System.Threading.Tasks;

namespace MindboxTest.Handlers.Tests
{
    public class AddFigureHandlerTests
    {
        private readonly ProxyFigureValidator _validator = new ProxyFigureValidator(new ProxyFigureStorage());

        static AddFigureHandlerTests()
        {
            ProxyFigureStorage.AddFigureProcessors(new CircleValidator(), new CircleCalculator(), "circle");
        }

        [Test]
        public async Task Handle_ValidatorReturnFail_Fail()
        {
            var handler = new AddFigureHandler(null, _validator);

            Result<AddFigureResponseData> result = await handler.Handle(
                new AddFigureRequest
                {
                    FigureDescription = new CircleDescription { Radius = -1 },
                    FigureType = "circle",
                },
                CancellationToken.None);

            Assert.True(result.Fail);
        }

        [Test]
        public async Task Handle_HappyPath_SavedToDb()
        {
            var queryFactoryMock = new Mock<IQueryFactory>();
            queryFactoryMock.Setup(x => x.SaveFigure(It.IsAny<Figure>()))
                .Returns(Task.FromResult(1L));

            var handler = new AddFigureHandler(queryFactoryMock.Object, _validator);

            Result<AddFigureResponseData> result = await handler.Handle(
                new AddFigureRequest
                {
                    FigureDescription = new CircleDescription { Radius = 1 },
                    FigureType = "circle",
                },
                CancellationToken.None);

            queryFactoryMock.Verify(x => x.SaveFigure(It.IsAny<Figure>()), Times.Once);
        }

        [Test]
        public async Task Handle_HappyPath_ReturnId()
        {
            long id = 1;

            var queryFactoryMock = new Mock<IQueryFactory>();
            queryFactoryMock.Setup(x => x.SaveFigure(It.IsAny<Figure>()))
                .Returns(Task.FromResult(id));

            var handler = new AddFigureHandler(queryFactoryMock.Object, _validator);

            Result<AddFigureResponseData> result = await handler.Handle(
                new AddFigureRequest
                {
                    FigureDescription = new CircleDescription { Radius = 1 },
                    FigureType = "circle",
                },
                CancellationToken.None);

            Assert.AreEqual(id, result.Data.FigureId);
        }
    }
}

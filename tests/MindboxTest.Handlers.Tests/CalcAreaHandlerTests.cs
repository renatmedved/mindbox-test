using MindboxTest.Contracts.Results;
using MindboxTest.DAL.QueryFactory;
using MindboxTest.DAL.Tables;
using MindboxTest.Figures.Circle;
using MindboxTest.Figures.Proxy;
using MindboxTest.Handlers.CalcArea;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace MindboxTest.Handlers.Tests
{
    public class CalcAreaHandlerTests
    {
        private readonly ProxyFigureStorage _storage = new ProxyFigureStorage();
        private readonly ProxyFigureValidator _validator;
        private readonly ProxyFigureDescriptionProvider _descriptionProvider;
        private readonly ProxyFigureCalculator _calculator;

        static CalcAreaHandlerTests()
        {
            ProxyFigureStorage.AddFigureProcessors(new CircleValidator(), new CircleCalculator(), "circle");
        }

        public CalcAreaHandlerTests()
        {
            _validator = new ProxyFigureValidator(_storage);
            _calculator = new ProxyFigureCalculator(_storage, _validator);
            _descriptionProvider = new ProxyFigureDescriptionProvider(_storage);
        }

        [Test]
        public async Task FigureNotFoundInDb_Fail()
        {
            var queryFactoryMock = new Mock<IQueryFactory>();
            queryFactoryMock
                .Setup(x => x.RetrieveFigure(It.IsAny<long>()))
                .Returns(Task.FromResult((Figure)null));

            var handler = new CalcAreaHandler(queryFactoryMock.Object, _calculator, _descriptionProvider);

            Result<CalcAreaResponseData> result = 
                await handler.Handle(new CalcAreaRequest(), CancellationToken.None);

            Assert.True(result.Fail);
        }

        [Test]
        public async Task DescriptionTypeNotFound_Fail()
        {
            var queryFactoryMock = new Mock<IQueryFactory>();
            queryFactoryMock
                .Setup(x => x.RetrieveFigure(It.IsAny<long>()))
                .Returns(Task.FromResult(new Figure { Type = "unknown" }));

            var handler = new CalcAreaHandler(queryFactoryMock.Object, _calculator, _descriptionProvider);

            Result<CalcAreaResponseData> result =
                await handler.Handle(new CalcAreaRequest(), CancellationToken.None);

            Assert.True(result.Fail);
        }

        [Test]
        public async Task ErrorDescription_Fail()
        {
            var queryFactoryMock = new Mock<IQueryFactory>();
            queryFactoryMock
                .Setup(x => x.RetrieveFigure(It.IsAny<long>()))
                .Returns(Task.FromResult(new Figure { Type = "circle", Description = "{radius:-1}" }));

            var handler = new CalcAreaHandler(queryFactoryMock.Object, _calculator, _descriptionProvider);

            Result<CalcAreaResponseData> result =
                await handler.Handle(new CalcAreaRequest(), CancellationToken.None);

            Assert.True(result.Fail);
        }

        [Test]
        public async Task RightDescription_Success()
        {
            var queryFactoryMock = new Mock<IQueryFactory>();
            queryFactoryMock
                .Setup(x => x.RetrieveFigure(It.IsAny<long>()))
                .Returns(Task.FromResult(new Figure { Type = "circle", Description = "{radius: 1}" }));

            var handler = new CalcAreaHandler(queryFactoryMock.Object, _calculator, _descriptionProvider);

            Result<CalcAreaResponseData> result =
                await handler.Handle(new CalcAreaRequest(), CancellationToken.None);

            Assert.True(result.Success);
        }
    }
}

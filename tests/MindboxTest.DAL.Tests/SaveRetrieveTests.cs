using MindboxTest.DAL.Connections;
using MindboxTest.DAL.Dto;
using MindboxTest.DAL.Queries;
using MindboxTest.DAL.Tables;
using MindboxTest.TestHelpers;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MindboxTest.DAL.Tests
{
    public class SaveRetrieveTests
    {
        [Test]
        public async Task SaveAndRetrieve()
        {
            var conFactory = new ConnectionFactory("DB/figures.sqlite");
            var saveQuery = new SaveFigureQuery(conFactory);

            var figure = new Figure
            {
                Type = "triangle"
            };

            var @params = new[] {
                new FigureAreaParam 
                {
                    ParameterName = "base",
                    ParameterValue = 1.1
                },
                new FigureAreaParam 
                {
                    ParameterName = "height",
                    ParameterValue = 2.2
                },
            };

            long figureId = await saveQuery.Execute(new FigureWithParamsDto(figure, @params));

            var retrieveQuery = new RetrieveFigureQuery(conFactory);

            FigureWithParamsDto retrieved = await retrieveQuery.Execute(figureId);

            Assert.IsNotNull(retrieved);
            Assert.AreEqual(figure.Type, retrieved.Figure.Type);
            Assert.AreEqual(figureId, retrieved.Figure.Id);

            Assert.AreEqual(2, retrieved.Params.Count());
            
            foreach(FigureAreaParam retrievedParam in retrieved.Params)
            {
                Assert.AreEqual(figureId, retrievedParam.FigureId);

                FigureAreaParam originalParam = @params
                    .FirstOrDefault(x => x.ParameterName == retrievedParam.ParameterName);

                Assert.NotNull(originalParam);

                Assert.That(originalParam.ParameterValue, Is.EqualTo(retrievedParam.ParameterValue).Within(DoubleHelpers.Tolerance));
            }
        }
    }
}
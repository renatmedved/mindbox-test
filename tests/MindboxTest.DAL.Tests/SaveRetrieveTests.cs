using MindboxTest.DAL.Connections;
using MindboxTest.DAL.Queries;
using MindboxTest.DAL.Tables;

using NUnit.Framework;

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
                Type = "triangle",
                Description = "{json}"
            };

            long figureId = await saveQuery.Execute(figure);

            var retrieveQuery = new RetrieveFigureQuery(conFactory);

            Figure retrieved = await retrieveQuery.Execute(figureId);

            Assert.IsNotNull(retrieved);
            Assert.AreEqual(figure.Type, retrieved.Type);
            Assert.AreEqual(figureId, retrieved.Id);
            Assert.AreEqual(figure.Description, retrieved.Description);
        }
    }
}
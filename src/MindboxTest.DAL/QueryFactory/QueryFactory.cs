using MindboxTest.DAL.Connections;
using MindboxTest.DAL.Queries;
using MindboxTest.DAL.Tables;

using System.Threading.Tasks;

namespace MindboxTest.DAL.QueryFactory
{
    public class QueryFactory : IQueryFactory
    {
        private readonly ConnectionFactory _conn;

        public QueryFactory(ConnectionFactory conn)
        {
            _conn = conn;
        }
        public Task<Figure> RetrieveFigure(long id)
        {
            var query = new RetrieveFigureQuery(_conn);

            return query.Execute(id);
        }

        public Task<long> SaveFigure(Figure figure)
        {
            var query = new SaveFigureQuery(_conn);

            return query.Execute(figure);
        }
    }
}

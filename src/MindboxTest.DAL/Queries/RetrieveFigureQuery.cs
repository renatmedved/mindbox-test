using Dapper;

using MindboxTest.DAL.Connections;
using MindboxTest.DAL.Tables;

using System.Data.SQLite;
using System.Threading.Tasks;

namespace MindboxTest.DAL.Queries
{
    public class RetrieveFigureQuery
    {
        private readonly ConnectionFactory _connFactory;

        public RetrieveFigureQuery(ConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<Figure> Execute(long id)
        {
            using SQLiteConnection conn = _connFactory.MakeDbConnection();
            await conn.OpenAsync();

            Figure figure = await conn.QueryFirstOrDefaultAsync<Figure>(
                @"SELECT Id, Type, Description
                  FROM Figure
                  WHERE Id = @id", new { id });

            return figure;
        }
    }
}

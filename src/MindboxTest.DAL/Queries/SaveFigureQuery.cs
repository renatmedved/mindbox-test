using Dapper;

using MindboxTest.DAL.Connections;
using MindboxTest.DAL.Tables;

using System.Data.SQLite;
using System.Threading.Tasks;

namespace MindboxTest.DAL.Queries
{
    public class SaveFigureQuery
    {
        private readonly ConnectionFactory _connFactory;

        public SaveFigureQuery(ConnectionFactory connFactory)
        {
            _connFactory = connFactory;
        }

        public async Task<long> Execute(Figure figure)
        {
            using SQLiteConnection conn = _connFactory.MakeDbConnection();
            await conn.OpenAsync();

            long id = await conn.QuerySingleAsync<long>(
                @"INSERT INTO Figure
                ( Type, Description ) 
                VALUES
                ( @Type, @Description );
                select last_insert_rowid()", figure);

            return id;
        }
    }
}

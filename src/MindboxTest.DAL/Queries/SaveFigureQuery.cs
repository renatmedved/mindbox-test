using Dapper;

using MindboxTest.DAL.Connections;
using MindboxTest.DAL.Dto;
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

        public async Task<long> Execute(FigureWithParamsDto dto)
        {
            using SQLiteConnection conn = _connFactory.MakeDbConnection();
            await conn.OpenAsync();

            long id = await conn.QuerySingleAsync<long>(
                @"INSERT INTO Figure
                ( Type ) VALUES
                ( @Type );
                select last_insert_rowid()", dto.Figure);

            foreach(FigureAreaParam param in dto.Params)
            {
                param.FigureId = id;

                await conn.QueryAsync(
                    @"INSERT INTO FigureAreaParam 
                    ( FigureId, ParameterName, ParameterValue ) VALUES
                    ( @FigureId, @ParameterName, @ParameterValue )", param);
            }

            return id;
        }
    }
}

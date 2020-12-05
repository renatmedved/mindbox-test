using Dapper;

using MindboxTest.DAL.Connections;
using MindboxTest.DAL.Dto;
using MindboxTest.DAL.Tables;

using System.Collections.Generic;
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

        public async Task<FigureWithParamsDto> Execute(long id)
        {
            using SQLiteConnection conn = _connFactory.MakeDbConnection();
            await conn.OpenAsync();

            Figure figure = await conn.QueryFirstAsync<Figure>(
                @"SELECT Id, Type 
                  FROM Figure
                  WHERE Id = @id", new { id });

            if (figure == null)
            {
                return null;
            }

            IEnumerable<FigureAreaParam> @params = await conn.QueryAsync<FigureAreaParam>(
                @"SELECT FigureId, ParameterName, ParameterValue
                  FROM FigureAreaParam
                  WHERE FigureId = @id", new { id });

            return new FigureWithParamsDto(figure, @params);
        }
    }
}

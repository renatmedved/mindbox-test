using MindboxTest.DAL.Tables;

using System.Threading.Tasks;

namespace MindboxTest.DAL.QueryFactory
{
    public interface IQueryFactory
    {
        Task<Figure> RetrieveFigure(long id);
        Task<long> SaveFigure(Figure figure);
    }
}

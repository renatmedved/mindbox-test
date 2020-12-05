using System.Data.SQLite;

namespace MindboxTest.DAL.Connections
{
    public class ConnectionFactory
    {
        private readonly string _dbFile;

        public ConnectionFactory(string dbFile)
        {
            _dbFile = dbFile;
        }

        public SQLiteConnection MakeDbConnection()
        {
            return new SQLiteConnection("Data Source=" + _dbFile);
        }
    }
}

using Microsoft.Data.SqlClient;

namespace Order.Infrastructure.Persistence.Factories
{
    public class SqlConnectionFactory(string conncetion)
    {
        private readonly string _conncetion = conncetion;
        public SqlConnection Create() => new(_conncetion);
    }
}

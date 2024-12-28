using Microsoft.Data.SqlClient;

namespace Orders.Infrastructure.Persistence.Factories
{
    public class SqlConnectionFactory(string connection)
    {
        private readonly string _connection = connection;
        public SqlConnection Create() => new(_connection);
    }
}

using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Sweeter.Services.ConnectionFactory
{
    public class ConnectionFactory : IConnectionFactory
    {
        private ConnectionStrings _string;

        public ConnectionFactory(IOptions<ConnectionStrings> ConnectionString)
        {
            this._string = ConnectionString.Value;
        }

        public SqlConnection CreateConnection
        {
            get
            {
                return new SqlConnection(_string.DefaultConnection);
            }
        }
    }
}

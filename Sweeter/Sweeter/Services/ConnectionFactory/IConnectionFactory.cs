using System.Data.SqlClient;

namespace Sweeter.Services.ConnectionFactory
{
    public interface IConnectionFactory
    {
        SqlConnection CreateConnection { get; }
    }
}

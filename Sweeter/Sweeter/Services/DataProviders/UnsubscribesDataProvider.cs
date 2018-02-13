using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sweeter.Services.ConnectionFactory;
using Sweeter.Models;
using Dapper;


namespace Sweeter.Services.DataProviders
{
    public class UnsubscribesDataProvider : IUnsubscribesDataProvider
    {
        private IConnectionFactory factory;

        public UnsubscribesDataProvider(IConnectionFactory connection)
        {
            factory = connection;
        }

        public void AddUnsubscribe(UnsubscribesModel unsubscribe)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"insert into UnsubscribesTable(IDus_ac, IDus_pas)
                values (@IDus_ac, @IDus_pas);",
                new { IDus_ac=unsubscribe.IDus_ac,IDus_pas=unsubscribe.IDus_pas });
            }
        }

        public void DeleteUnsubscribe(int IDus_ac, int? IDus_pas)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"delete from UnsubscribesTable where IDus_ac = @IDus_ac and IDus_pas=@IDus_pas", new { IDus_ac=IDus_ac, IDus_pas=IDus_pas });
            }
        }

        public IEnumerable<UnsubscribesModel> GetUnsubscribes(int IDus_ac, int? IDus_pas)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var unsubscribes = sqlConnection.Query<UnsubscribesModel>("select * from UnsubscribesTable where IDus_ac=@IDus_ac and IDus_pas=@IDus_pas", new { IDus_ac = IDus_ac, IDus_pas = IDus_pas }).ToList();
                return unsubscribes;
            }
        }

        public IEnumerable<UnsubscribesModel> GetUnsubscribesOfUser(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var unsubscribes = sqlConnection.Query<UnsubscribesModel>("select * from UnsubscribesTable where IDus_ac=@IDus_ac", new { IDus_ac=id }).ToList();
                return unsubscribes;
            }
        }
    }
}

using Sweeter.Models;
using System.Collections.Generic;

namespace Sweeter.Services.DataProviders
{
    public interface IUnsubscribesDataProvider
    {
        void AddUnsubscribe(UnsubscribesModel unsubscribe);
        IEnumerable<UnsubscribesModel> GetUnsubscribesOfUser(int id);
        IEnumerable<UnsubscribesModel> GetUnsubscribes(int IDus_ac, int? IDus_pas);
        void DeleteUnsubscribe(int IDus_ac, int? IDus_pas);
    }
}

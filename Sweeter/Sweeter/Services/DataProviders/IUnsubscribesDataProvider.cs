using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sweeter.Models;

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

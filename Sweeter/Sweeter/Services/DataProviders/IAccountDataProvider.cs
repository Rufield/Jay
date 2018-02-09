using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sweeter.DataProviders
{
    using Models;
   public interface IAccountDataProvider
    {
        
        IEnumerable<AccountModel> GetAccounts();
        IEnumerable<AccountModel> GetAccountsByEmail(string Email);
        IEnumerable<AccountModel> GetAccountsByUsername(string username);
        AccountModel GetAccount(int id);
        AccountModel GetAccountByEmail(string Email);
        void AddAccount(AccountModel account);

        void UpdateAccount(AccountModel account);

        void DeleteAccount(int id);
    }
}

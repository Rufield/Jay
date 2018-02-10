using System.Collections.Generic;


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

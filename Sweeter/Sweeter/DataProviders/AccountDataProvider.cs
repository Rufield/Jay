using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sweeter.Models;
using Dapper;
using System.Data.SqlClient;





namespace Sweeter.DataProviders
{
    
    public class AccountDataProvider : IAccountDataProvider
    {
        
        string connectionString  =System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
       
        private SqlConnection sqlConnection;

        public void AddAccount(AccountModel account)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Execute(@"insert into AccountTable(Name, Email, Password, Username, Avatar)
      values (@Name, @Email, @Password, @Username, @Avatar);",
    new { Name=account.Name, Email=account.Email,Password= account.Password, Username=account.Username, Avatar=account.Avatar });
           
            }

        }

        public void DeleteAccount(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Execute(@"delete from AccountTable where IDuser = @id", new { id = id });
            }
        }

        public AccountModel GetAccount(int id)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var account = sqlConnection.Query<AccountModel>("select * from AccountTable where IDuser = @id", new { id = id }).First();
                return account;
            }
        }

        public AccountModel GetAccountByEmail(string Email)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var account = sqlConnection.Query<AccountModel>("select * from AccountTable where Email=@email", new { email = Email }).First();
                return account;
            }
        }
        public IEnumerable<AccountModel> GetAccounts()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var accounts = sqlConnection.Query<AccountModel>("select * from AccountTable").ToList();
                return accounts;
            }
        }
        public IEnumerable<AccountModel> GetAccountsByEmail(string Email)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var accounts = sqlConnection.Query<AccountModel>("select * from AccountTable where Email=@Email",new { Email = Email }).ToList();
                return accounts;
            }
        }
        public IEnumerable<AccountModel> GetAccountsByUsername(string username)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var accounts = sqlConnection.Query<AccountModel>("select * from AccountTable where Username=@username", new { username=username }).ToList();
                return accounts;
            }
        }

        public void UpdateAccount(AccountModel account)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {

                sqlConnection.Execute(@"update AccountTable set Fullname=@Fullname, Email=@Email, Password=@Password, Username=@Username, Avatar=@Avatar where ID = @id;",
              new { Fullname= account.Name, Email= account.Email, Password= account.Password,Username= account.Username, Avatar=account.Avatar, id= account.IDuser });
            }
        }
    }
}

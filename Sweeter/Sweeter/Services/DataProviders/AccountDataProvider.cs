﻿using Dapper;
using Sweeter.Models;
using Sweeter.Services.ConnectionFactory;
using System.Collections.Generic;
using System.Linq;

namespace Sweeter.DataProviders
{
    public class AccountDataProvider : IAccountDataProvider
    {
        private IConnectionFactory factory;

        public AccountDataProvider(IConnectionFactory connection)
        {
            factory = connection;
        }

        public void AddAccount(AccountModel account)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute(@"insert into AccountTable(Name, Email, Password, Username, Avatar, Style)
                values (@Name, @Email, @Password, @Username, @Avatar, @Style);",
                new { Name = account.Name, Email = account.Email, Password = account.Password, Username = account.Username, Avatar = account.Avatar, Style = account.Style});
            }
        }

        public IEnumerable<AccountModel> SearchAccountsByUsername(string username)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                IEnumerable<AccountModel> accounts = sqlConnection.Query<AccountModel>("select * from AccountTable").ToList();
                accounts = accounts.Where(x=>x.Username.Contains(username.ToLower())|| x.Username.Contains(username.ToUpper()));
                return accounts;
            }
        }

        public void DeleteAccount(int? id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                sqlConnection.Execute("delete from AccountTable where IDuser = @id", new { id = id });
            }
        }

        public AccountModel GetAccount(int id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var account = sqlConnection.Query<AccountModel>("select * from AccountTable where IDuser = @id", new { id = id }).First();
                return account;
            }
        }
        public AccountModel GetAccount(int? id)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var account = sqlConnection.Query<AccountModel>("select * from AccountTable where IDuser = @id", new { id = id }).First();
                return account;
            }
        }
        public AccountModel GetAccountByEmail(string Email)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var account = sqlConnection.Query<AccountModel>("select * from AccountTable where Email=@email", new { email = Email }).First();
                return account;
            }
        }
        public IEnumerable<AccountModel> GetAccounts()
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var accounts = sqlConnection.Query<AccountModel>("select * from AccountTable").ToList();
                return accounts;
            }
        }
        public IEnumerable<AccountModel> GetAccountsByEmail(string Email)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var accounts = sqlConnection.Query<AccountModel>("select * from AccountTable where Email=@Email",new { Email = Email }).ToList();
                return accounts;
            }
        }
        public IEnumerable<AccountModel> GetAccountsByUsername(string username)
        {
            using (var sqlConnection = factory.CreateConnection)
            {
                var accounts = sqlConnection.Query<AccountModel>("select * from AccountTable where Username=@username", new { username=username }).ToList();
                return accounts;
            }
        }

        public void UpdateAccount(AccountModel account)
        {
            using (var sqlConnection = factory.CreateConnection)
            {

                sqlConnection.Execute(@"update AccountTable set Name=@Name, Username=@Username, Avatar=@Avatar, Style=@Style, About=@About where iduser = @iduser;",
                new { Name = account.Name, Username= account.Username, Avatar=account.Avatar, Style=account.Style, About=account.About, iduser = account.IDuser });
            }
        }
    }
}

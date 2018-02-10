using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sweeter.Models;
using Dapper;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Sweeter.DataProviders;




// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
 
   
    [Route("/account")]
    public class AccountController : Controller
    {
        // GET: /<controller>/
       

      /*  private IAccountDataProvider accountDataProvider;
        public AccountController(IAccountDataProvider accountData)
        {
            this.accountDataProvider = accountData;
        }

        [HttpGet("{id}")]
        public async Task<AccountModel> Get(int id)
        { 
          return await this.accountDataProvider.GetAccount(id);
        }



        [HttpPost]
        public async Task Post([FromBody]AccountModel account)
        {
            await this.accountDataProvider.AddAccount(account);
            
        }
       
    */
    }
}

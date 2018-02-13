using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Sweeter.Models;
using Sweeter.Services.DataProviders;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sweeter.Controllers
{
    [Route("/Unsubscribe")]
    public class UnsubscribeController : Controller
    {
        private IUnsubscribesDataProvider unsubscribesDataProvider;
        public UnsubscribeController( IUnsubscribesDataProvider unsubscribesData)
        {
          
            this.unsubscribesDataProvider = unsubscribesData;
        }
        // GET: /<controller>/
       [HttpGet]
        public RedirectResult Subscribe(int? id)
        {
            int idUs = int.Parse(HttpContext.User.FindFirst(x => x.Type == "Current").Value);
            if (unsubscribesDataProvider.GetUnsubscribes(idUs, id).Count() == 0)
            {
                UnsubscribesModel unsubscribe = new UnsubscribesModel
                {
                    IDus_ac = idUs,
                    IDus_pas = id
                };
                unsubscribesDataProvider.AddUnsubscribe(unsubscribe);
                return Redirect("/UserPage?id=" + id);
            }
            else
            {
                unsubscribesDataProvider.DeleteUnsubscribe(idUs, id);
                return Redirect("/UserPage?id=" + id);
            }
        }
    }
}

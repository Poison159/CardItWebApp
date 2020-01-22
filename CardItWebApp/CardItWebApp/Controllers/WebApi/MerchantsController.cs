using CardItWebApp.Database;
using CardItWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CardItWebApp.Controllers.WebApi
{
    public class MerchantsController : ApiController
    {
        private CardItContext dbContext = new CardItContext();

        // GET api/merchant/x
        public Merchant Get(int id)
        {
            var merchant = dbContext.Merchants.Where(x => x.Id == id).FirstOrDefault();

            return merchant;
        }
    }
}

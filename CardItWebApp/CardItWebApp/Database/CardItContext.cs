using CardItWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CardItWebApp.Database
{
    public class CardItContext : DbContext
    {
        public CardItContext() :base("CardIt")
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Merchant> Merchants { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardItWebApp.Models
{
    public class Card
    {
        public int Id { get; set; }

        //public User User{ get; set; }

        public Merchant Merchant { get; set; }

        public string CardNumber { get; set; }

    }
}
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
    public class CardsController : ApiController
    {
        private CardItContext dbContext = new CardItContext();

        // GET api/cards
        public IEnumerable<Card> Get()
        {
            var cards = dbContext.Cards.ToList();

            return cards;
        }

        // GET api/cards/5
        public Card Get(int id)
        {
            var card = dbContext.Cards.Where(x => x.Id == id).FirstOrDefault();

            return card;
        }

        // POST api/cards
        public void Post([FromBody]string value)
        {
            //How can one add a new Card??
            //Already have user info
            //Already selected the merchant
        }

        // PUT api/cards/x
        public IHttpActionResult Put(Card card)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            var existingCard = dbContext.Cards
                                        .Where(s => s.Id == card.Id)
                                        .FirstOrDefault<Card>();

            if (existingCard != null)
            {
                existingCard.CardNumber = card.CardNumber;

                dbContext.SaveChanges();
            }
            else
                return NotFound();

            return Ok();
        }

        // DELETE api/cards/5
        public void Delete(int id)
        {
            if (id <= 0)
                return ;

             var card = dbContext.Cards
                                 .Where(s => s.Id == id)
                                 .FirstOrDefault();

            dbContext.Entry(card).State = System.Data.Entity.EntityState.Deleted;
            dbContext.SaveChanges();
        }

    }
}

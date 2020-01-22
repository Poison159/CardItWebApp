using CardItWebApp.Database;
using CardItWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CardItWebApp.Controllers
{
    public class UsersController : ApiController
    {
        private CardItContext dbContext = new CardItContext();
        

        // GET api/users/x
        public User Get(int id)
        {
            var user = dbContext.Users.Where(x => x.Id == id).FirstOrDefault();

            return user;
        }

        // POST api/users
        public void Post([FromBody]string value)
        {
           
        }

        // PUT api/users/x
        public IHttpActionResult Put(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            var existingUser = dbContext.Users
                                        .Where(s => s.Id == user.Id)
                                        .FirstOrDefault<User>();

            if (existingUser != null)
            {
                existingUser.MobileNumber   = user.MobileNumber;
                existingUser.Email          = user.Email;

                dbContext.SaveChanges();
            }
            else
                return NotFound();

            return Ok();
        }

        // DELETE api/users/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid User id");

            var user = dbContext.Users
                                 .Where(s => s.Id == id)
                                 .FirstOrDefault();

            dbContext.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            dbContext.SaveChanges();

            return Ok();
        }

    }
}

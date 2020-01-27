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
        public IHttpActionResult Get(int id)
        {
            var user = dbContext.Users.Where(x => x.Id == id).FirstOrDefault();

            if (user is null)
                //User doesn't exist
                return NotFound();

            return Ok(user);
        }

        [Route("api/GetRegisterUser")]
        public IHttpActionResult GetRegisterUser(string name, string email, string mobileNumber, string password)
        {
            //Check if user exists
            var encryptedPassword = EncryptPassword(password);

            var user = dbContext.Users.Where(x => x.Password == encryptedPassword).FirstOrDefault();

            if (user != null)
                //User already exists
                return NotFound();

            User newUser = new User
            {
                Name = name,
                Email = email,
                MobileNumber = mobileNumber,
                Password = EncryptPassword(password)
            };

            dbContext.Users.Add(newUser);

            dbContext.SaveChanges();

            newUser.Password = null;

            return Ok(newUser);
        }

        [Route("api/GetUserLogin")]
        public IHttpActionResult GetUserLogin(string name, string email, string mobileNumber, string password)
        {
            //Check if user exists
            var encryptedPassword = EncryptPassword(password);

            var user = dbContext.Users.Where(x => x.Password == encryptedPassword).FirstOrDefault();

            if (user is null)
                //User doesn't exist
                return NotFound();

            bool applyEdits = false;

            //Check for changes
            if (user.Name != name)
            {
                user.Name = name;
                applyEdits = true;
            }

            if (user.Email != email)
            {
                user.Email = email;
                applyEdits = true;
            }

            if (user.MobileNumber != mobileNumber)
            {
                user.MobileNumber = mobileNumber;
                applyEdits = true;
            }

            if (applyEdits)
                dbContext.SaveChanges();

            user.Password = null;

            return Ok(user);

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

        public string EncryptPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String encryptedPasswordHash = System.Text.Encoding.ASCII.GetString(data);

            return encryptedPasswordHash;
        }
    }
}

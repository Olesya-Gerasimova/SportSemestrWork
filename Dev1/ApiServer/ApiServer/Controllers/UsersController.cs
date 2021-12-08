using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ApiServer.Models;
using System.Threading.Tasks;
using ApiServer.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        PremierLeagueContext db;
        public UsersController(PremierLeagueContext context)
        {
            db = context;
            if (!db.Users.Any())
            {
                db.Users.Add(new User {Username = "o.gerrr", Password = HashingHelper.HashUsingPbkdf2("Jktcz2002") });
                db.Users.Add(new User {Username = "sofa_pom", Password = HashingHelper.HashUsingPbkdf2("ilovecucumber") });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await db.Users.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<User>> Get(string username)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }



        // POST api/users
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Ok(user);
        }
    }
}

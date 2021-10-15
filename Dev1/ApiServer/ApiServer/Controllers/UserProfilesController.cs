using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfilesController : ControllerBase
    {
        PremierLeagueContext db;
        public  UserProfilesController(PremierLeagueContext context)
        {
            db = context;
            if (!db.UserProfiles.Any())
            {
                User user1 =  db.Users.FirstOrDefault(x => x.Username == "o.gerrr");
                if (user1 != null)
                    db.UserProfiles.Add(new UserProfile { Name = "Olesya", Surname = "Gerasimova", Email = "olesy_2002@mail.ru", Bio = "student", BirthDate = new DateTime(2002, 03, 06), UserId = user1.Id, User = user1});
                User user2 = db.Users.FirstOrDefault(x => x.Username == "sofa_pom");
                if (user2 != null)
                    db.UserProfiles.Add(new UserProfile { Name = "sofa", Surname = "pomeranskaya", Email = "sofa_pom_dog@mail.ru", Bio = "dog", BirthDate = new DateTime(2015, 08, 06), UserId = user2.Id, User = user2 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> Get()
        {
            return await db.UserProfiles.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<UserProfile>> Post(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                return BadRequest();
            }

            db.UserProfiles.Add(userProfile);
            await db.SaveChangesAsync();
            return Ok(userProfile);
        }
        [HttpPut]
        public async Task<ActionResult<UserProfile>> Put(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                return BadRequest();
            }
            if (!db.UserProfiles.Any(x => x.Id == userProfile.Id))
            {
                return NotFound();
            }

            db.Update(userProfile);
            await db.SaveChangesAsync();
            return Ok(userProfile);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserProfile>> Delete(int id)
        {
           UserProfile userProfile = db.UserProfiles.FirstOrDefault(x => x.Id == id);
            if (userProfile == null)
            {
                return NotFound();
            }
            db.UserProfiles.Remove(userProfile);
            await db.SaveChangesAsync();
            return Ok(userProfile);
        }


    }
}

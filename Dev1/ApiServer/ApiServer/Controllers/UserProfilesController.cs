using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiServer.Interfaces;
using ApiServer.Requests;
using Microsoft.AspNetCore.Authorization;

namespace ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileService userProfileService;
        public  UserProfilesController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUserProfile([FromBody] UserProfileRequest userProfileRequest)
        {
            if (userProfileRequest == null || string.IsNullOrEmpty(userProfileRequest.Username) ||
                string.IsNullOrEmpty(userProfileRequest.UserId) || string.IsNullOrEmpty(userProfileRequest.Token))
            {
                return BadRequest("Missing user details to retrieve associated profile!");
            }
            Console.WriteLine("Received request for retrieving profile for: " + 
                              userProfileRequest.Username + 
                              userProfileRequest.UserId);
            var userProfileResponse = await userProfileService.GetProfileForUser(userProfileRequest);
            if (userProfileResponse == null)
            {
                return NotFound();
            }
            Console.WriteLine("Returning profile with 200");
            Console.WriteLine("Profile: " + userProfileResponse.Name + userProfileResponse.Surname);

            return Ok(userProfileResponse);
        }
        
        [HttpPost]
        [HttpPut]
        public async Task<IActionResult> CreateUpdateProfileForUser([FromBody] UserProfileCreateUpdateRequest userProfileRequest)
        {
            if (userProfileRequest == null || string.IsNullOrEmpty(userProfileRequest.Username) ||
                string.IsNullOrEmpty(userProfileRequest.UserId) || string.IsNullOrEmpty(userProfileRequest.Token))
            {
                return BadRequest("Missing user details to create/update user profile!");
            }
            Console.WriteLine("Received request to create a new profile: " + 
                              userProfileRequest.Email + 
                              userProfileRequest.Name + 
                              userProfileRequest.Surname + userProfileRequest.Token + 
                              userProfileRequest.UserId + userProfileRequest.BirthDate
                              );
            var userProfileResponse = await userProfileService.CreateUpdateProfileForUser(userProfileRequest);
            return Ok(userProfileResponse);
        }
        
/*
        [HttpPost]
        public async Task<ActionResult<UserProfile>> Post([FromBody] UserProfile userProfile)
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
        public async Task<ActionResult<UserProfile>> Put([FromBody] UserProfile userProfile)
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
*/

    }
}

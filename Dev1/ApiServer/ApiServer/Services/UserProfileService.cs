using System;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Helpers;
using ApiServer.Interfaces;
using ApiServer.Models;
using ApiServer.Requests;
using ApiServer.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly PremierLeagueContext db;

        public UserProfileService(PremierLeagueContext premierLeagueDbContext)
        {
            this.db = premierLeagueDbContext;
        }

        public async Task<UserProfileResponse> GetProfileForUser(UserProfileRequest userProfileRequest)
        {
            var userProfile = await db.UserProfiles.FirstOrDefaultAsync(x => x.UserId == Convert.ToInt32(userProfileRequest.UserId));
            if (userProfile == null)
            {
                Console.WriteLine("Not found profile for " + userProfileRequest.Username + userProfileRequest.UserId);
                return null;
            }
            Console.WriteLine("Found profile: " + userProfile.Name + userProfile.Surname);
            return new UserProfileResponse() { 
                Username = userProfileRequest.Username, 
                Id = Convert.ToInt32(userProfileRequest.UserId), 
                Token = userProfileRequest.Token,
                Name = userProfile.Name,
                Surname = userProfile.Surname,
                Bio = userProfile.Bio,
                Email = userProfile.Email,
                BirthDate = userProfile.BirthDate.ToString()
            };
        }

        public async Task<UserProfileResponse> CreateUpdateProfileForUser(UserProfileCreateUpdateRequest createUpdateRequest)
        {
            var userProfile = await db.UserProfiles.FirstOrDefaultAsync(x => x.UserId == Convert.ToInt32(createUpdateRequest.UserId));
            
            if (userProfile == null)
            {
                UserProfile up = new UserProfile { Name = createUpdateRequest.Name, 
                    Surname = createUpdateRequest.Surname,
                    Bio = createUpdateRequest.Bio,
                    BirthDate = Convert.ToDateTime(createUpdateRequest.BirthDate),
                    Email = createUpdateRequest.Email,
                    UserId = Convert.ToInt32(createUpdateRequest.UserId)
                };
                db.UserProfiles.Add(up);
                await db.SaveChangesAsync();
            }
            else
            {
                userProfile.Name = createUpdateRequest.Name;
                userProfile.Surname = createUpdateRequest.Surname;
                userProfile.Email = createUpdateRequest.Email;
                userProfile.Bio = createUpdateRequest.Bio;
                userProfile.BirthDate = Convert.ToDateTime(createUpdateRequest.BirthDate);
                userProfile.UserId = Convert.ToInt32(createUpdateRequest.UserId);
                db.UserProfiles.Update(userProfile);
                await db.SaveChangesAsync();
            }
            return new UserProfileResponse
            {
                Name = createUpdateRequest.Name,
                Surname = createUpdateRequest.Surname,
                Bio = createUpdateRequest.Bio,
                BirthDate = createUpdateRequest.BirthDate,
                Email = createUpdateRequest.Email,
                Id = Convert.ToInt32(createUpdateRequest.UserId),
                Token = createUpdateRequest.Token,
                Username = createUpdateRequest.Username
            };
        }
    }
}

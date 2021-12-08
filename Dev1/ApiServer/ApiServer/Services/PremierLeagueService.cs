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
    public class PremierLeagueService : IPremierLeagueService
    {
        private readonly PremierLeagueContext db;

        public PremierLeagueService(PremierLeagueContext premierLeagueDbContext)
        {
            this.db = premierLeagueDbContext;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Username == loginRequest.Username);
            if (user == null)
            {
                return null;
            }
            var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password);
            if (user.Password != passwordHash)
            {
                return null;
            }
            var token = await Task.Run(() => TokenHelper.GenerateToken(user));
            return new LoginResponse { Username = user.Username, Id = user.Id, Token = token };
        }

        public async Task<SignUpResponse> AddUser(SignUpRequest signUpRequest)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Username == signUpRequest.Username);
            if (user == null)
            {
                db.Users.Add(new User {Username = signUpRequest.Username,
                    Password = HashingHelper.HashUsingPbkdf2(signUpRequest.Password) });
                db.SaveChanges();
                return new SignUpResponse { Status = "Success" };
            }
            Console.WriteLine("User already exists");
            return new SignUpResponse { Status = "Error" };
        }

    }
}

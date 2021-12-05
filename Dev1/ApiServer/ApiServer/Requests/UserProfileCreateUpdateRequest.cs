using System;
using ApiServer.Models;

namespace ApiServer.Requests
{
    public class UserProfileCreateUpdateRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
    }
}

using System;
namespace ApiServer.Requests
{
    public class UserProfileRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}

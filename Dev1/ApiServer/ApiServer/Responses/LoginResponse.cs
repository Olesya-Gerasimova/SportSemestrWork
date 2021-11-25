using System;
namespace ApiServer.Responses
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public int Id { get; set; }
    }
}

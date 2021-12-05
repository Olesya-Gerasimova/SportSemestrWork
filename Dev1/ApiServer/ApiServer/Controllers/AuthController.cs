using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServer.Interfaces;
using ApiServer.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiServer.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IPremierLeagueService premierLeagueService;

        public AuthController(IPremierLeagueService premierLeagueService)
        {
            this.premierLeagueService = premierLeagueService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) ||
                string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Missing login details");
            }

            var loginResponse = await premierLeagueService.Login(loginRequest);
            if (loginResponse == null)
            {
                return BadRequest($"Invalid credentials");
            }

            return Ok(loginResponse);
        }
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            if (signUpRequest == null || string.IsNullOrEmpty(signUpRequest.Password) ||
                string.IsNullOrEmpty(signUpRequest.Username))
            {
                return BadRequest("Missing sign up info");
            }
            var signUpResponse = await premierLeagueService.AddUser(signUpRequest);
            if (signUpResponse.Status.Equals("Error"))
            {
                return BadRequest("Username already exists! Please use different username");
            }
            return Ok(signUpResponse);
        }
    }
}

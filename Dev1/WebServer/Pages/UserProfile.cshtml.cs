using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ApiServer.Requests;
using ApiServer.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebServer.Pages
{
    public class UserProfileModel : PageModel
    {
        static private string UserProfileUrl = "https://localhost:5001/api/userprofiles";
        
        [BindProperty(Name = "Id", SupportsGet = true)]
        public string UserId { get; set; }

        [BindProperty(Name = "Username", SupportsGet = true)]
        public string Username { get; set; }

        [BindProperty(Name = "Token", SupportsGet = true)]
        public string Token { get; set; }
        public UserProfileResponse upr { get; set; }

        static HttpClient httpClient = new HttpClient();

        public IActionResult OnGet()
        {
            // return URI of the created resource.
            Console.WriteLine(Username);
            var postUserProfile = new UserProfileRequest() { 
                Username = Username, 
                UserId = UserId, 
                Token = Token
            };
            var userProfileRequest = new HttpRequestMessage(HttpMethod.Get, UserProfileUrl)
            {
                Content = JsonContent.Create(postUserProfile),
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", Token)}
            };
            var userProfileResponse = httpClient.Send(userProfileRequest);
            Console.WriteLine(userProfileResponse.StatusCode);
            if (userProfileResponse.IsSuccessStatusCode)
            {
                var responseBody = userProfileResponse.Content.ReadAsStream();
                using var streamReader = new StreamReader(responseBody);
                using var jsonReader = new JsonTextReader(streamReader);
                JsonSerializer serializer = new JsonSerializer();
                UserProfileResponse lr = serializer.Deserialize<UserProfileResponse>(jsonReader);
                Console.WriteLine("Id: " + lr.Id + ", Token: " + lr.Token + ", Username: " + lr.Username);
                Console.WriteLine("Found up: " + lr.Name + lr.Surname
                    + lr.Bio + lr.Email + lr.BirthDate);
                DateTime transformDateTime = DateTime.Parse(lr.BirthDate);
                string jsBirthDate = transformDateTime.ToString("yyyy-MM-dd");
                this.upr = new UserProfileResponse
                {
                    Id = lr.Id, Username = lr.Username, Token = lr.Token,
                    Name = lr.Name, Surname = lr.Surname,
                    Bio = lr.Bio, Email = lr.Email, BirthDate = jsBirthDate
                };
                return Page();
            }

            if (userProfileResponse.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Not found yser profile, return empty");
                this.upr = new UserProfileResponse { Id = Convert.ToInt32(UserId), Username = Username, Token = Token,
                    Name = "", Surname = "",
                    Bio = "", Email = "", BirthDate = null };
            }
            return Page();
        }

        public IActionResult OnGetUserDetails()
        {
            return this.OnGet();
        }

        public IActionResult OnPost(string Name, string Surname, string Bio, string Email, DateTime? BirthDate)
        {
            Console.WriteLine("Received new user profile");
            Console.WriteLine(Name);
            Console.WriteLine(Surname);
            Console.WriteLine(Bio);
            Console.WriteLine(Email);
            Console.WriteLine(BirthDate);

            // return URI of the created resource.
            var postUserProfile = new UserProfileCreateUpdateRequest() { 
                Username = this.Username, 
                UserId = this.UserId, 
                Token = this.Token,
                Name = Name,
                Surname = Surname,
                Bio = Bio,
                Email = Email,
                BirthDate = BirthDate.ToString()
            };
            var userProfileRequest = new HttpRequestMessage(HttpMethod.Post, UserProfileUrl)
            {
                Content = JsonContent.Create(postUserProfile),
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", Token) }
            };
            var userProfileCreateUpdateResponse = httpClient.Send(userProfileRequest);
            Console.WriteLine(userProfileCreateUpdateResponse.StatusCode);
            if (userProfileCreateUpdateResponse.IsSuccessStatusCode)
            {
                var responseBody = userProfileCreateUpdateResponse.Content.ReadAsStream();
                using var streamReader = new StreamReader(responseBody);
                using var jsonReader = new JsonTextReader(streamReader);
                JsonSerializer serializer = new JsonSerializer();
                UserProfileResponse lr = serializer.Deserialize<UserProfileResponse>(jsonReader);
                Console.WriteLine("Id: " + lr.Id + ", Token: " + lr.Token + ", Username: " + lr.Username);
                DateTime transformDateTime = DateTime.Parse(lr.BirthDate);
                string jsBirthDate = transformDateTime.ToString("yyyy-MM-dd");
                this.upr = new UserProfileResponse
                {
                    Id = lr.Id, Username = lr.Username, Token = lr.Token,
                    Name = lr.Name, Surname = lr.Surname,
                    Bio = lr.Bio, Email = lr.Email, BirthDate = jsBirthDate
                };
                return Page();
            }
            return Page();
        }
    }
}

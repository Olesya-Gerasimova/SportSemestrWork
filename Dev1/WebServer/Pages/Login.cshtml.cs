using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiServer.Requests;
using System.Net.Http.Json;
using ApiServer.Models;
using System.IO;
using Newtonsoft.Json;
using ApiServer.Responses;

namespace WebServer.Pages
{
    public class LoginModel : PageModel
    {
        public string Message = "Введите Логин и Пароль";
        public string ExceptionMessage { get; set; }
        
        static HttpClient httpClient = new HttpClient();

        static private string LoginUrl = "https://localhost:5001/api/auth/login";

        public void OnGet()
        {
            ExceptionMessage = "";
        }

        public IActionResult OnPost(string Username, string Password)
        {
            // return URI of the created resource.
            Console.WriteLine(Username);
            Console.WriteLine(Password);
            var postUser = new LoginRequest() { Username = Username, Password = Password };
            var loginRequest = new HttpRequestMessage(HttpMethod.Post, LoginUrl)
            {
                Content = JsonContent.Create(postUser)
            };
            var loginResponse = httpClient.Send(loginRequest);
            Console.WriteLine(loginResponse.StatusCode);
            if (loginResponse.IsSuccessStatusCode)
            {
                var responseBody = loginResponse.Content.ReadAsStream();
                using var streamReader = new StreamReader(responseBody);
                using var jsonReader = new JsonTextReader(streamReader);
                JsonSerializer serializer = new JsonSerializer();
                LoginResponse lr = serializer.Deserialize<LoginResponse>(jsonReader);
                Console.WriteLine("Id: " + lr.Id + ", Token: " + lr.Token + ", Username: " + lr.Username);
                return new RedirectToPageResult("/UserProfile", "UserDetails",
                    new { Id = lr.Id, Username = lr.Username, Token = lr.Token });
            }
            ExceptionMessage = "Неверно введены username или пароль!.";
            return Page();
        }
    }
}

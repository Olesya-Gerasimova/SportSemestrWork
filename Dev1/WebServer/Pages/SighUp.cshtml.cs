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
    public class SighUpModel : PageModel
    {
        public string Message = "Чтобы зарегистрироваться, введите Логин и Пароль";
        public string ExceptionMessage { get; set; }
        

        static HttpClient httpClient = new HttpClient();

        static private string SignUpUrl = "https://localhost:5001/api/auth/signup";

        public void OnGet()
        {
            ExceptionMessage = "";
        }

        public IActionResult OnPost(string Username, string Password)
        {
            // return URI of the created resource.
            Console.WriteLine(Username);
            Console.WriteLine(Password);
            var postUser = new SignUpRequest { Username = Username, Password = Password };
            var signUpRequest = new HttpRequestMessage(HttpMethod.Post, SignUpUrl)
            {
                Content = JsonContent.Create(postUser)
            };
            var signUpResposne = httpClient.Send(signUpRequest);
            Console.WriteLine(signUpResposne.StatusCode);
            if (signUpResposne.IsSuccessStatusCode)
            {
                return new RedirectToPageResult("/Login");
            }
            ExceptionMessage = "Невозможно зарегистрироваться. Пользователь с таким Username уже существует.";
            return Page();
        }
    }
}

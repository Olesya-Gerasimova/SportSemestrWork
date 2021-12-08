using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebServer.Pages
{
    public class NewsDetailsModel : PageModel
    {
        private readonly ILogger<NewsDetailsModel> _logger;

        public NewsDetailsModel(ILogger<NewsDetailsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}

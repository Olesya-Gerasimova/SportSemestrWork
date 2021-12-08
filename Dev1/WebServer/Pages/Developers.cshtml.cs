using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebServer.Pages
{
    public class DevelopersModel : PageModel
    {
        private readonly ILogger<DevelopersModel> _logger;

        public DevelopersModel(ILogger<DevelopersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}

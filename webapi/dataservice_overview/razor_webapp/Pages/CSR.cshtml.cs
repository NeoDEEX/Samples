using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace razor_webapp.Pages
{
    public class CSRModel : PageModel
    {
        private readonly ILogger<CSRModel> _logger;

        public CSRModel(ILogger<CSRModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}

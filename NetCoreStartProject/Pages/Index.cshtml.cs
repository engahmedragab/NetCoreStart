using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NetCoreStartProject.Domain;

namespace NetCoreStartProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<User> _signInManager;


        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor, SignInManager<User> signInManager
            )
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
            var eee = _signInManager;
            var yy = User;
            var message = _httpContextAccessor.HttpContext.Request.PathBase;
        }
    }
}

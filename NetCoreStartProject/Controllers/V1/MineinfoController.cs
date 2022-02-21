using Microsoft.AspNetCore.Mvc;

namespace NetCoreStartProject.Controllers.V1
{
    public class MineinfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace NetCoreStartProject.Controllers.V1
{
    public class LookupsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

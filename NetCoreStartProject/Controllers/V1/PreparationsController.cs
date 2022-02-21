using Microsoft.AspNetCore.Mvc;

namespace NetCoreStartProject.Controllers.V1
{
    public class PreparationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

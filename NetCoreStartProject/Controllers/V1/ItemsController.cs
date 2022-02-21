using Microsoft.AspNetCore.Mvc;

namespace NetCoreStartProject.Controllers.V1
{
    public class ItemsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace NetCoreStartProject.Controllers.V1
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

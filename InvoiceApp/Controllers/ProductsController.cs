using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

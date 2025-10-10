using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

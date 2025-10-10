using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.Controllers
{
    public class InvoicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

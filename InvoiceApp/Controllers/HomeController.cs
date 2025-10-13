using InvoiceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Data;
using InvoiceApp.ViewModels;

namespace InvoiceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Customer)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                TotalInvoices = invoices.Count,

                // Sum Total only for paid invoices (runs in memory)
                TotalRevenue = invoices
                    .Where(i => i.Status == InvoiceStatus.Paid)
                    .Sum(i => i.Total),

                PaidInvoices = invoices.Count(i => i.Status == InvoiceStatus.Paid),
                OverdueInvoices = invoices.Count(i => i.Status == InvoiceStatus.Overdue),

                // Get recent invoices sorted by date
                RecentInvoices = invoices
                    .OrderByDescending(i => i.InvoiceDate)
                    .Take(5)
                    .ToList()
            };


            return View(viewModel);
        }


    }
}

// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Data;
using InvoiceApp.ViewModels;
using InvoiceApp.Models;

namespace InvoiceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Landing page - accessible to everyone
        public IActionResult Index()
        {

            // If user is already logged in, redirect to dashboard
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Dashboard));
            }

            return View();
        }

        // Dashboard - requires authentication
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            // Load all invoices with related data needed for calculations
            var invoices = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceItems)
                .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                TotalInvoices = invoices.Count,
                TotalRevenue = invoices
                    .Where(i => i.Status == InvoiceStatus.Paid)
                    .Sum(i => i.Total), // Now works in memory
                PaidInvoices = invoices
                    .Count(i => i.Status == InvoiceStatus.Paid),
                OverdueInvoices = invoices
                    .Count(i => i.Status == InvoiceStatus.Overdue),
                RecentInvoices = invoices
                    .OrderByDescending(i => i.InvoiceDate)
                    .Take(5)
                    .ToList()
            };

            return View(viewModel);

           // return View(viewModel);
        }
    }
}
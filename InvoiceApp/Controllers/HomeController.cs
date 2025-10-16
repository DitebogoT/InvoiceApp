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
            var viewModel = new DashboardViewModel
            {
                TotalInvoices = await _context.Invoices.CountAsync(),
                TotalRevenue = await _context.Invoices
                    .Where(i => i.Status == InvoiceStatus.Paid)
                    .SumAsync(i => i.Total),
                PaidInvoices = await _context.Invoices
                    .CountAsync(i => i.Status == InvoiceStatus.Paid),
                OverdueInvoices = await _context.Invoices
                    .CountAsync(i => i.Status == InvoiceStatus.Overdue),
                RecentInvoices = await _context.Invoices
                    .Include(i => i.Customer)
                    .OrderByDescending(i => i.InvoiceDate)
                    .Take(5)
                    .ToListAsync()
            };

            return View(viewModel);
        }
    }
}
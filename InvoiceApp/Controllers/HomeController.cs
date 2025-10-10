using System.Diagnostics;
using InvoiceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
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

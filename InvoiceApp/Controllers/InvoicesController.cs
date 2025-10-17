using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Data;
using InvoiceApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceApp.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Customer)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();
            return View(invoices);
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.InvoiceItems)
                    .ThenInclude(ii => ii.Product)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);

            if (invoice == null) return NotFound();

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name");
            ViewBag.Products = _context.Products.ToList();

            var invoice = new Invoice
            {
                InvoiceNumber = GenerateInvoiceNumber(),
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30)
            };

            return View(invoice);
        }

        // POST: Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Invoice invoice, List<int> productIds, List<int> quantities, List<decimal> unitPrices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();

                // Add invoice items
                for (int i = 0; i < productIds.Count; i++)
                {
                    if (quantities[i] > 0)
                    {
                        var item = new InvoiceItem
                        {
                            InvoiceId = invoice.InvoiceId,
                            ProductId = productIds[i],
                            Quantity = quantities[i],
                            UnitPrice = unitPrices[i]
                        };
                        _context.InvoiceItems.Add(item);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = invoice.InvoiceId });
            }

            ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name", invoice.CustomerId);
            ViewBag.Products = _context.Products.ToList();

            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var invoice = await _context.Invoices
                .Include(i => i.InvoiceItems)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null) return NotFound();

            ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name", invoice.CustomerId);
            ViewBag.Products = _context.Products.ToList();
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Invoice invoice, List<int> productIds, List<int> quantities, List<decimal> unitPrices)
        {
            if (id != invoice.InvoiceId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Remove existing items
                    var existingItems = _context.InvoiceItems.Where(ii => ii.InvoiceId == id);
                    _context.InvoiceItems.RemoveRange(existingItems);

                    // Add updated items
                    for (int i = 0; i < productIds.Count; i++)
                    {
                        if (quantities[i] > 0)
                        {
                            var item = new InvoiceItem
                            {
                                InvoiceId = invoice.InvoiceId,
                                ProductId = productIds[i],
                                Quantity = quantities[i],
                                UnitPrice = unitPrices[i]
                            };
                            _context.InvoiceItems.Add(item);
                        }
                    }

                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Details), new { id = invoice.InvoiceId });
            }

            ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name", invoice.CustomerId);
            ViewBag.Products = _context.Products.ToList();
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);

            if (invoice == null) return NotFound();

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.InvoiceId == id);
        }

        private string GenerateInvoiceNumber()
        {
            var lastInvoice = _context.Invoices.OrderByDescending(i => i.InvoiceId).FirstOrDefault();
            var nextNumber = (lastInvoice?.InvoiceId ?? 0) + 1;
            return $"INV-{DateTime.Now.Year}-{nextNumber:D5}";
        }
    }
}
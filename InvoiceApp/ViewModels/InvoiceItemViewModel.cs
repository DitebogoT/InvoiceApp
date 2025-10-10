using InvoiceApp.Models;

namespace InvoiceApp.ViewModels
{
    public class InvoiceItemViewModel
    {
        public Invoice Invoice { get; set; }
        public List<InvoiceItemViewModel> Items { get; set; } = new();
    }

    public class InvoiceItemModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }

    public class DashboardViewModel
    {
        public int TotalInvoices { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PaidInvoices { get; set; }
        public int OverdueInvoices { get; set; }
        public List<Invoice> RecentInvoices { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace InvoiceApp.Models
{
    [Table("Invoices")]
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [Required]
        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public String InvoiceItem { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; }


        [Range(0, 100)]
        public decimal TaxRate { get; set; } = 15; // Default 15%

        [Range(0, double.MaxValue)]
        public decimal Discount { get; set; } = 0;

        public string Notes { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

        // Calculated properties
        public decimal SubTotal => InvoiceItems?.Sum(i => i.Total) ?? 0; 
        public decimal TaxAmount => SubTotal * (TaxRate / 100);
        public decimal Total => SubTotal + TaxAmount - Discount;
    }
    public class InvoiceItem
    {
        public int InvoiceItemId { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public decimal UnitPrice { get; set; }

        // Foreign key and navigation property
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int Total { get; internal set; }
        public Product Product { get; set; }
    }
}

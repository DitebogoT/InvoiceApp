using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

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
}

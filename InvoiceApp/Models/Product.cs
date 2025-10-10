using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}

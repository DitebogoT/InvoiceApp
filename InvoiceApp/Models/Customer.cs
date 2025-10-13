using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public ICollection<Invoice> Invoices { get; set; }
    }
}

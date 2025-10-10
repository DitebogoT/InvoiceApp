using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}

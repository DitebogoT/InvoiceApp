using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
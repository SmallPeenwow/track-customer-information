using System.ComponentModel.DataAnnotations;

namespace track_customer_information.Models
{
    public class CustomerModel
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Address { get; set; }

        public string? TelephoneNumber { get; set; }

        public string? ContactPersonName { get; set; }

        [EmailAddress]
        public string? ContactPersonEmail { get; set; }

        public string? VATNumber { get; set; }
    }
}

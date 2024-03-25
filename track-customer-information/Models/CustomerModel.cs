using System.ComponentModel.DataAnnotations;

namespace track_customer_information.Models
{
    public class CustomerModel
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Please enter in your Name.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Please enter in your Address.")]
        public required string Address { get; set; }

        public string? TelephoneNumber { get; set; }

        public string? ContactPersonName { get; set; }

        [Required(ErrorMessage = "Please enter in your Email.")]
        [EmailAddress]
        public string? ContactPersonEmail { get; set; }

        public string? VATNumber { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace Assignment_1.Models
{
    public class Customers
    {
        

        
        //just making get and set for data that will happen whena customer account is created
        public int CustomersId { get; set; }

        public string FirstName { get; set; } 
        [Required(ErrorMessage = "Please enter a first name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter a Last name.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter a Password.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a Email.")]

    }
}

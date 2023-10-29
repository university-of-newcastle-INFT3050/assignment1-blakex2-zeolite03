using System;
using System.ComponentModel.DataAnnotations;
//info for the cusomer
namespace INFT3050_project.Models.Product
{
    public class Customer
    {
        //just making get and set for data that will happen whena customer account is created
        public int CustomersId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a Last name.")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter a Password.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please enter a Email.")]
        public string? Email { get; set; }
        




    }
}

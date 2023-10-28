using INFT3050_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace INFT3050_project.ViewModels
{
    public class OrderViewModel
    {

        [Required]
        public string Email { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string postcode { get; set; }
        [Required]
        public string Suburb { get; set; }
        [Required]
        public string state { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string CardOwner { get; set; }
        [Required]
        public string Expiry { get; set; }
        [Required]
        public int CVV { get; set; }
        [Required]
        public string StreetAddress { get; set; }

    }
}

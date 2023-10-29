using INFT3050_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace INFT3050_project.ViewModels
{
    public class OrderViewModel
    {

        [Required(ErrorMessage = "Please Enter an Email Address")]
        [EmailAddress]
        [StringLength(50)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Enter an Address")]
        [StringLength(50)]
        public string address { get; set; }


        [Required(ErrorMessage = "Please Enter an Postcode")]
        [StringLength(4)]
        public string postcode { get; set; }

        [Required(ErrorMessage = "Please Enter an Suburb")]
        [StringLength(20)]
        public string Suburb { get; set; }


        [Required(ErrorMessage = "Please Enter an state")]
        [StringLength(20)]
        public string state { get; set; }


        [Required(ErrorMessage = "Please Enter an PhoneNumber")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Enter an Card Number")]
        [StringLength(20)]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please Enter an Card Owner ")]
        [StringLength(20)]
        [Display(Name = "Card Owner")]
        public string CardOwner { get; set; }


        [Required(ErrorMessage = "Please Enter an Expiry")]
        [StringLength(20)]
        public string Expiry { get; set; }


        [Required(ErrorMessage = "Please Enter an CVV ")]
        public int CVV { get; set; }

        [Required(ErrorMessage = "Please Enter an StreetAddress ")]
        public string StreetAddress { get; set; }

    }
}

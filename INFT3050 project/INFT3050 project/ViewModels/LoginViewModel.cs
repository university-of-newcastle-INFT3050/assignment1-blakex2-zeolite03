using INFT3050_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace INFT3050_project.ViewModels
{
    public class LoginViewModel
    {

      
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter a Password")]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string HashPW { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage ="Please Enter an Email Address")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter a Name")]
        [StringLength(20)]
        public string Name { get; set; }
       
    }
}

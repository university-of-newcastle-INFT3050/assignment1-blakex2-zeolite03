using INFT3050_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace INFT3050_project.ViewModels
{
    public class LoginViewModel
    {

        
        public string UserName { get; set; }
        [Required]
        public string HashPW { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}

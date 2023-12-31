﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
//created by Eveleigh 6/08/2023 WIP
namespace INFT3050_project.Models
{
    // these are the employees of the company
    // there would be role stuff here if we got to that point.
    public class User
    {
        [Key]
        public int UserId { get; set; }

        
        [Required(ErrorMessage = "Please enter a User name.")]
        [MaxLength(50)]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Please enter a Email.")]
        //[MaxLength(255)]
        [AllowNull]
        public string? Email { get; set; } = "";


       

        //the sql data will populate but we will still need to make a hash function and slat function for the patron model

        //SHA256 hash value of salt + password (password appended to salt value)
        [Required(ErrorMessage = "Please enter a password.")]
        [MaxLength(64)]
        public string HashPW { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(255)]
        public string Name { get; set; }
        
        //if true employee is an admin
        public bool IsAdmin { get; set; }

        //32 hexadecimal digits generated at random
        [MaxLength(32)]
        public string Salt { get; set; }
        

    }
}

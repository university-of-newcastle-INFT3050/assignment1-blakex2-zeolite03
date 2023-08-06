using System.ComponentModel.DataAnnotations;
//created by Eveleigh 6/08/2023 WIP
namespace INFT3050_project.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Key]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter a User name.")]
        [MaxLength(50)]

        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a Email.")]
        [MaxLength(255)]

        // ask Connor how to possibly add the salt and hash to this user and other models

        //the sql data will populate but we will still need to make a hash function and slat function for the patron model

        //SHA256 hash value of salt + password (password appended to salt value)
        public string HashedPW { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [MaxLength(64)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(255)]
        public bool IsAdmin { get; set; }
                
        //32 hexadecimal digits generated at random
        public string Salt { get; set; }
        [MaxLength(32)]

    }
}

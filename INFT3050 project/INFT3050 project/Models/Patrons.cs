using System.ComponentModel.DataAnnotations;
//created by Eveleigh 6/08/2023 WIP
namespace INFT3050_project.Models
{
    public class Patrons
    {
        [Key]
        public int UserId { get; set; }

        //email is used as a username
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a Email.")]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a  name.")]
        [MaxLength(255)]

        //so the hash function and salt function will need to be created for when a new patron creates an account

        //32 hexadecimal digits generated at random
        public string Salt { get; set; }
        [MaxLength(32)]

        //SHA256 hash value of salt + password (password appended to salt value)

        public string HashedPW { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [MaxLength(64)]
    } 
}

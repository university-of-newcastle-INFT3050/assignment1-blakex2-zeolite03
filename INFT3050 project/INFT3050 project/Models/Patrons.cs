using System.ComponentModel.DataAnnotations;
//created by Eveleigh 6/08/2023 WIP
namespace INFT3050_project.Models
{
    public class Patrons
    {
        [Key]
        public int UserId { get; set; }

        //email is used as a username
        [Required(ErrorMessage = "Please enter a Email.")]
        [MaxLength(255)]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "Please enter a  name.")]
        [MaxLength(255)]
        public string Name { get; set; }


        //so the hash function and salt function will need to be created for when a new patron creates an account

        //32 hexadecimal digits generated at random
        [MaxLength(32)]
        public string Salt { get; set; }


        //SHA256 hash value of salt + password (password appended to salt value)
        [Required(ErrorMessage = "Please enter a password.")]
        [MaxLength(64)]
        public string HashedPW { get; set; }
      
    }
}

using System.ComponentModel.DataAnnotations;
//created by Eveleigh 6/08/2023
namespace INFT3050_project.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Key]
        public string UserName { get; set; }
        
        public string Email { get; set; }

        // ask Connor how to possibly add the salt and hash to this user and other models

        //the sql data will populate but we will still need to make a hash function and slat function for the patron model
        public string HashedPW { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
                
        public int Salt { get; set; }

    }
}

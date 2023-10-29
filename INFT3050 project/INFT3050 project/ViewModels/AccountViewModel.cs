using System.ComponentModel.DataAnnotations;

namespace INFT3050_project.ViewModels
{
    public class AccountViewModel
    {
        public string UserId { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        
        public string Name { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword
        {
            get; set;

        }

    }
}

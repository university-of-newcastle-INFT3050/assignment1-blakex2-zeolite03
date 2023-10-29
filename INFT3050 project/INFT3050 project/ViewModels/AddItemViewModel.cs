using System.ComponentModel.DataAnnotations;


namespace INFT3050_project.ViewModels
{
    public class AddItemViewModel
    {
        // adding an item
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Description for the item")]
        [StringLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the author")]
        [StringLength(20)]
        public string Author { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace INFT3050_project.Models.Product.Subgenre
{
    public interface ISubGenre
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

    }
}

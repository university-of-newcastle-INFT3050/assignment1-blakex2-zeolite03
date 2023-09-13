using System.ComponentModel.DataAnnotations.Schema;

namespace INFT3050_project.Models.Product.Subgenre
{
    [Table("Movie_genre")]
    public class Movie_Genre : ISubGenre
    {
        [Column("subGenreID")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
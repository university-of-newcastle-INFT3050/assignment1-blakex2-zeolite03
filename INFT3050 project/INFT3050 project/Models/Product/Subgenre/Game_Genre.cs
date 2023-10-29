using System.ComponentModel.DataAnnotations.Schema;
//info for the movie genre
namespace INFT3050_project.Models.Product.Subgenre
{
    [Table("Movie_Genre")]
    public class Game_Genre : ISubGenre
    {
        [Column("subGenreID")]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
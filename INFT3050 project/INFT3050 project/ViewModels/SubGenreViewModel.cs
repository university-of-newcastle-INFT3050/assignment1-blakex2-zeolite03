using INFT3050_project.Models.Product;
using INFT3050_project.Models.Product.Subgenre;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace INFT3050_project.ViewModels
{
    public class SubGenreViewModel
    {
        public List<Genre> Genres { get; set; }

        public List<ISubGenre> book_Genres { get; set; }
        public List<ISubGenre> game_Genres { get; set; }
        public List<ISubGenre> movie_Genres { get; set; }


        //public List<> GetSubGreListByGenre(IDataTokensMetadata genre
        public List<ISubGenre> GetSubGenreByGenreId(int genreId)
        {
            switch (genreId)
            {
                case 1:
                    return book_Genres;
                case 2:
                    return movie_Genres;
                case 3:
                    return game_Genres;
                default:
                    break;
            }
            return new List<ISubGenre>();
        }
    }
}

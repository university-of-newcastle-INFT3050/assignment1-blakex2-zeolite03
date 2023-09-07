using INFT3050_project.Models.Product.Subgenre;
using INFT3050_project.ViewModels;

namespace INFT3050_project.Models.Managers
{
    public static class ShopManager
    {
        public static SubGenreViewModel GetViewModel(ShopContext context)
        {
            SubGenreViewModel vm = new SubGenreViewModel()
            {
                Genres = context.Genre.ToList(),
                book_Genres = context.Book_Genres.Cast<ISubGenre>().ToList(),
                movie_Genres = context.Movie_Genres.Cast<ISubGenre>().ToList(),
                game_Genres = context.Game_Genres.Cast<ISubGenre>().ToList(),
            };
            return vm;
        }
    }
}

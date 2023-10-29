using INFT3050_project.Models.Stocktake;
using System.ComponentModel.DataAnnotations;

namespace INFT3050_project.ViewModels
{
    // dont think this is used
    public class StocktakeViewModel
    {
        public Stocktake stocktake { get; set; }
        public Source Source { get; set; }
    }
}

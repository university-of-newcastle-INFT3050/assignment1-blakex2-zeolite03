namespace INFT3050_project.Models.Stocktake

{
    //info on the stocktake
    public class Stocktake
    {
       
        public int itemid { get; set; }
        public int SourceId { get; set; }

        public int ProductId { get; set; }
       
        public int Quantity { get; set; }
        public double Price { get; set; }
        
    }
}

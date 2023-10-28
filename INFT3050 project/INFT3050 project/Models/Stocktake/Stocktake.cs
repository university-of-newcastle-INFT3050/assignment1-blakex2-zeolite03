namespace INFT3050_project.Models.Stocktake
{
    public class Stocktake
    {
        public int ItemID { get; set; }
        public int SourceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}

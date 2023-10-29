using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFT3050_project.Models.Order
{
    //info for the product in orders
    public class ProductsInOrders
    {
        [Key]
        public int ProductsInOrdersId { get; set; }
        public int OrderId { get; set; }
        //[ForeignKey("OrderId")]
        //public Orders orders { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }

       // [ForeignKey("ProductId")]
        //public Stocktake.Stocktake stocktake { get; set; }

    }
}

using INFT3050_project.Models.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFT3050_project.Models.Order
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int customer { get; set; }
        [ForeignKey("customer")]
       


        public string StreetAddress { get; set; }
        public string postcode { get; set; }
        public string Suburb { get; set; }
        public string state { get; set; }

    }
}

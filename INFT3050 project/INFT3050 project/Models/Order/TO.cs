using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFT3050_project.Models.Order
{
    public class TO
    {
        [Key]
        public int customerID { get; set; }
        public int PatronId { get; set; }
        [ForeignKey("PatronId")]
        
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string Suburb { get; set; }
        public string CardNumber { get; set; }
        public string CardOwner { get; set; }
        public string Expiry { get; set; }
        public int CVV { get; set; }   

    }
}

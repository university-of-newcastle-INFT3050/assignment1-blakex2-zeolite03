using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFT3050_project.Models.Order
{
    //info for the TO
    public class TO
    {

        // i think the code in the controller saves but i havent tested properly 
        //since it partially incomplete havent bothered with text validation
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

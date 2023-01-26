using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    public class CustomerProduct
    {
        [Key]
        [Display(Name = "CustomerProductID")]
        public int CustomerProductID { get; set; }

        [Required]
        [Display(Name = "CustomerID")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "ProductID")]
        public int ProductID { get; set; }

        [Display(Name = "CustomerID")]
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [Display(Name = "ProductID")]
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    public class ProductOrder
    {
        [Key]
        [Display(Name = "ProductOrderID")]
        public int ProductOrderID { get; set; }

        [Required]
        [Display(Name = "ProductID")]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "OrderID")]
        public int OrderID { get; set; }

        [Display(Name = "OrderID")]
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [Display(Name = "ProductID")]
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}

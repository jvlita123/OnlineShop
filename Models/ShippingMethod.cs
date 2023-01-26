using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    public class ShippingMethod
    {
        [Key]
        [Display(Name = "ShippingMethodID")]
        public int ShippingMethodID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

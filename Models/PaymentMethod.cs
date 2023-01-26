using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    public class PaymentMethod
    {
        [Key]
        [Display(Name = "PaymentMethodID")]
        public int PaymentMethodID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

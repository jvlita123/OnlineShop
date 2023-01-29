using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Sklep_MVC_Projekt.Models
{
    
    public class ShopAttribute
    {
        [Key]
        [Display(Name = "ShopAttributeID")]
        public int ShopAttributeID { get; set; }
        [Display(Name = "Attribute")]
        public string Attribute { get; set; }
        [Display(Name = "Value")]
        public string Value { get; set; }

    }
}


// pagecounter | 3